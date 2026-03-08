using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HumanDesign.Infrastructure.Entities;
using HumanDesign.Application.Interfaces;
using HumanDesign.Domain.Models.Requests;
using HumanDesign.Infrastructure.Data;

namespace HumanDesign.API.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController(
    AppDbContext db,
    UserManager<UserEntity> userManager,
    SignInManager<UserEntity> signInManager,
    IJwtService jwtService) : ControllerBase
{
    private readonly AppDbContext _db = db;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly IJwtService _jwtService = jwtService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto dto)
    {
        if (await _userManager.FindByEmailAsync(dto.Email) != null)
            return BadRequest("Email already exists");

        var prospect = _db.Prospects.Where(p => p.Email == dto.Email).Select(p => new { p.FullName, p.Phone, ParentId = p.OwnerId }).FirstOrDefault();

        var user = new UserEntity
        {
            UserName = dto.Email,
            Email = dto.Email,
            PhoneNumber = prospect!.Phone,
            Role = "User",
            FullName = prospect!.FullName,
            ParentId = prospect!.ParentId,
            ReferralCode = GenerateReferralCode()
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, "User");

        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return Unauthorized();

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

        if (!result.Succeeded)
            return Unauthorized();

        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
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

    public static string GenerateReferralCode(int length = 8)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string([.. Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)])]);
    }
}