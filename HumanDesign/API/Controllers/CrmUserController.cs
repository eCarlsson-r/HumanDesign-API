using HumanDesign.Application.Interfaces;
using HumanDesign.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanDesign.API.Controllers;
[Authorize]
[ApiController]
[Route("api/crm/users")]
public class CrmUserController(
    UserManager<UserEntity> userManager,
    IUserHierarchyService hierarchy) : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly IUserHierarchyService _hierarchy = hierarchy;

    [HttpGet("agents")]
    public async Task<IActionResult> GetAgents()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var roles = await _userManager.GetRolesAsync(currentUser!);

        IQueryable<UserEntity> query = _userManager.Users;

        if (roles.Contains("Admin"))
        {
            query = query.Where(u => u.Role == "Agent");
        }
        else if (roles.Contains("Leader"))
        {
            query = query.Where(u => u.ParentId == currentUser!.Id);
        }
        else
        {
            return Forbid();
        }

        var result = await query
            .Select(u => new
            {
                u.Id,
                u.FullName,
                u.Email,
                u.CreatedAt
            })
            .ToListAsync();

        return Ok(result);
    }
}