
using HumanDesign.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace HumanDesign.Application.Services.Helpers;
public class AuthService(AppDbContext db, UserManager<UserEntity> userManager, 
    SignInManager<UserEntity> signInManager, IJwtService jwt) : IAuthService
{
    private readonly AppDbContext _db = db;
    private readonly IJwtService _jwt = jwt;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    public async Task<IActionResult> RegisterAsync(string email, string password)
    {
        if (await _userManager.FindByEmailAsync(email) != null)
            return new BadRequestObjectResult("Email already exists");

        var prospect = _db.Prospects.Where(p => p.Email == email)
            .Select(p => new { p.FullName, p.Phone, p.Status, ParentId = p.OwnerId })
            .FirstOrDefault();

        var user = new UserEntity
        {
            UserName = email,
            Email = email,
            PhoneNumber = prospect!.Phone,
            Role = "User",
            FullName = prospect!.FullName,
            ParentId = prospect!.ParentId,
            ReferralCode = GenerateReferralCode()
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded) return new BadRequestObjectResult(result.Errors);

        await _userManager.AddToRoleAsync(user, "User");

        var token = _jwt.GenerateToken(user, prospect!.Status.ToString());
        return new OkObjectResult(new { token });
    }

    public async Task<IActionResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return new UnauthorizedResult();

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!result.Succeeded)
            return new UnauthorizedResult();

        var prospectStatus = _db.Prospects.Where(p => p.Email == user.Email).Select(p => p.Status).FirstOrDefault();

        var token = _jwt.GenerateToken(user, prospectStatus.ToString());

        return new OkObjectResult(new { token });
    }

    public static string GenerateReferralCode(int length = 8)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string([.. Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)])]);
    }
}