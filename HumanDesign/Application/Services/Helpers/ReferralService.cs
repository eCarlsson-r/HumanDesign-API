using Microsoft.EntityFrameworkCore;
using HumanDesign.Application.Interfaces;
using HumanDesign.Infrastructure.Data;

namespace HumanDesign.Application.Services.Helpers;
public class ReferralService(AppDbContext db) : IReferralService
{
    private readonly AppDbContext _db = db;

    public async Task<Guid> ResolveOwnerIdAsync(string? referralCode)
    {
        if (!string.IsNullOrWhiteSpace(referralCode))
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.ReferralCode == referralCode);

            if (user != null)
                return user.Id;
        }

        // fallback to Admin
        var admin = await _db.Users
            .FirstAsync(u => u.Role == "Admin");

        return admin.Id;
    }
}