using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HumanDesign.Infrastructure.Entities;
using HumanDesign.Domain.Models.Requests;
using HumanDesign.Application.Interfaces;

namespace HumanDesign.API.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController(
    IAuthService authService,
    UserManager<UserEntity> userManager) : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly IAuthService _auth = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto dto)
    {
        return await _auth.RegisterAsync(dto.Email, dto.Password);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        return await _auth.LoginAsync(dto.Email, dto.Password);
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null) return Unauthorized();

        return Ok(new
        {
            user.Id,
            user.FullName,
            user.Email
        });
    }
}