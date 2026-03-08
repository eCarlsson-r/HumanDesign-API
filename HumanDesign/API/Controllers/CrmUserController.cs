using HumanDesign.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Domain.Models.Requests;

namespace HumanDesign.API.Controllers;
[Authorize]
[ApiController]
[Route("api/crm/users")]
public class CrmUserController(
    UserManager<UserEntity> userManager,
    AppDbContext db) : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AppDbContext _db = db;

    [HttpGet("agents")]
    public async Task<IActionResult> GetAgents(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var roles = await _userManager.GetRolesAsync(currentUser!);

        IQueryable<UserEntity> query = _db.Users;

        if (roles.Contains("Admin"))
        {
            query = from u in _db.Users
                    join ur in _db.UserRoles on u.Id equals ur.UserId
                    join r in _db.Roles on ur.RoleId equals r.Id
                    where r.Name == "Agent" || r.Name == "Leader"
                    select u;
        }
        else if (roles.Contains("Leader"))
        {
            query = query.Where(u => u.ParentId == currentUser!.Id);
        }
        else
        {
            return Forbid();
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.FullName.Contains(search));
        }

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new
            {
                u.Id,
                u.FullName,
                u.Email,
                u.CreatedAt
            })
            .ToListAsync();

        return Ok(new { total, items });
    }

    [HttpGet("agents/{id}")]
    public async Task<IActionResult> GetAgent(Guid id)
    {
        Console.WriteLine("ID: " + id);
        var user = await _userManager.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        Console.WriteLine("User: " + user);

        if (user == null) return NotFound();
            
        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new
        {
            user.Id,
            user.FullName,
            user.Email,
            user.ParentId,
            user.CreatedAt,
            Role = roles.FirstOrDefault()
        });
    }

    [HttpGet("leaders")]
    public async Task<IActionResult> GetLeaders()
    {
        var leaders = await (
            from u in _db.Users
            join ur in _db.UserRoles on u.Id equals ur.UserId
            join r in _db.Roles on ur.RoleId equals r.Id
            where r.Name == "Leader"
            select new
            {
                u.Id,
                u.FullName
            }
        ).ToListAsync();

        return Ok(leaders);
    }

    [HttpPut("agents/{id}")]
    public async Task<IActionResult> UpdateAgent(string id, [FromBody] UpdateAgentDto dto)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return NotFound();

        user.FullName = dto.FullName;
        user.ParentId = dto.ParentId;
        user.Role = dto.Role;

        await _userManager.UpdateAsync(user);

        var roles = await _userManager.GetRolesAsync(user);

        await _userManager.RemoveFromRolesAsync(user, roles);
        await _userManager.AddToRoleAsync(user, dto.Role);

        return Ok();
    }
}