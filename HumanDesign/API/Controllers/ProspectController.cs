using HumanDesign.Application.Interfaces;
using HumanDesign.Domain.Enums;
using HumanDesign.Domain.Models.Requests;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HumanDesign.API.Controllers;
[Authorize]
[ApiController]
[Route("api/prospects")]
public class ProspectController(
    AppDbContext db, IProspectService service, IHumanDesignReportBuilder builder,
    IUserHierarchyService hierarchy, IReferralService referral,
    UserManager<UserEntity> userManager) : ControllerBase
{
    private readonly AppDbContext _db = db;
    private readonly IProspectService _service = service;
    private readonly IHumanDesignReportBuilder _builder = builder;
    private readonly IUserHierarchyService _hierarchy = hierarchy;
    private readonly IReferralService _referral = referral;
    private readonly UserManager<UserEntity> _userManager = userManager;

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var roles = await _userManager.GetRolesAsync(currentUser!);

        var query = _db.Prospects
            .Include(p => p.Owner)
            .AsQueryable();

        if (roles.Contains("Admin"))
        {
            // no filter
        }
        else if (roles.Contains("Leader") || roles.Contains("Agent"))
        {
            var descendants = await _hierarchy.GetDescendantUserIdsAsync(currentUser!.Id);
            descendants.Add(currentUser.Id);

            query = query.Where(p => descendants.Contains(p.OwnerId));
        }
        else if (roles.Contains("User"))
        {
            query = query.Where(p => p.OwnerId == currentUser!.Id);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p =>
                p.FullName.Contains(search) ||
                p.Email.Contains(search));
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new
            {
                p.Id,
                p.FullName,
                p.Email,
                p.Status,
                Owner = p.Owner.FullName,
                p.CreatedAt
            })
            .ToListAsync();

        return Ok(new { total, items });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetail(Guid id)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var roles = await _userManager.GetRolesAsync(currentUser!);

        var prospect = await _db.Prospects
            .Include(p => p.Owner)
            .Include(p => p.GeneratedDesign)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (prospect == null)
            return NotFound();

        // Role-based security
        if (!roles.Contains("Admin"))
        {
            var descendants = await _hierarchy.GetDescendantUserIdsAsync(currentUser!.Id);
            descendants.Add(currentUser.Id);

            if (!descendants.Contains(prospect.OwnerId))
                return Forbid();
        }

        return Ok(new
        {
            prospect.Id,
            prospect.FullName,
            prospect.Email,
            prospect.Phone,
            prospect.Status,
            prospect.CreatedAt,
            Owner = prospect.Owner.FullName,
            Design = new
            {
                prospect.GeneratedDesign?.Type,
                prospect.GeneratedDesign?.Authority,
                prospect.GeneratedDesign?.Definition,
                prospect.GeneratedDesign?.Profile
            }
        });
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> GeneratePreview(
        [FromQuery] string? referrer,
        [FromBody] CreateProspectRequest request
    )
    {
        var ownerId = await _referral.ResolveOwnerIdAsync(referrer);

        var prospectId = await _service.CreateProspectAsync(request, ownerId);

        var designId = await _db.Designs.Where(d => d.ProspectId == prospectId).Select(p => p.Id).FirstOrDefaultAsync();
        var report = await _builder.BuildPreviewAsync(designId);

        return Ok(new { prospectId, report });
    }
    
    [AllowAnonymous]
    [HttpPatch("{id}/unlock-summary")]
    public async Task<IActionResult> UnlockSummary(Guid id)
    {
        var prospect = await _db.Prospects.FindAsync(id);
        if (prospect == null)
            return NotFound();

        prospect.Status = ProspectStatus.SummaryUnlocked;

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = prospect.Email,
            UserName = prospect.Email,
            FullName = prospect.FullName,
            ParentId = prospect.OwnerId
        };

        await _userManager.CreateAsync(user, "1122334455");
        await _userManager.AddToRoleAsync(user, "User");

        await _db.SaveChangesAsync();

        return Ok();
    }
}