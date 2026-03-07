using HumanDesign.Application.Services.Reports;
using HumanDesign.Domain.Models.Reports;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanDesign.API.Controllers;
[Authorize]
[ApiController]
[Route("api/dashboard")]
public class DashboardController(DashboardService service, UserManager<UserEntity> userManager) : ControllerBase
{    private readonly DashboardService _service = service;
    private readonly UserManager<UserEntity> _userManager = userManager;

    [HttpGet("stats")]
    public async Task<DashboardStats> GetStats()
    {
        var user = await _userManager.GetUserAsync(User) ?? throw new Exception("User not found");
        return await _service.GetStatsAsync(user.Id);
    }
}