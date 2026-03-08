
using HumanDesign.Application.Interfaces;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanDesign.Application.Services.Helpers;
public class AuthService(AppDbContext db, IJwtService jwt) : IAuthService
{
    private readonly AppDbContext _db = db;
    private readonly IJwtService _jwt = jwt;

    public async Task<string> RegisterAsync(string email, string password)
    {
        if (await _db.Users.AnyAsync(u => u.Email == email))
            throw new Exception("Email already exists");

        var prospectData = await _db.Prospects.FirstOrDefaultAsync(p => p.Email == email) ?? throw new Exception("Registration can only be done for prospects.");
        var fullName = prospectData.FullName;
        var parentId = prospectData.OwnerId;

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            FullName = fullName,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = "User",
            ParentId = parentId,
            ReferralCode = Guid.NewGuid().ToString()[..8]
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return _jwt.GenerateToken(user);
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email)
            ?? throw new Exception("Invalid credentials");

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return _jwt.GenerateToken(user);
    }
}