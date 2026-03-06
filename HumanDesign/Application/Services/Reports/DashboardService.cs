using HumanDesign.Domain.Models.Reports;
using HumanDesign.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HumanDesign.Application.Services.Reports;

public class DashboardService(AppDbContext db)
{
    private readonly AppDbContext _db = db;

    public async Task<DashboardStats> GetStatsAsync(Guid id)
    {
        var user = await _db.Users.FindAsync(id) ?? throw new Exception("User not found");
        var userId = user.Id;
        var role = user.Role;

        var stats = new DashboardStats
        {
            Prospects = await _db.Prospects.CountAsync(),

            TeamMembers = await _db.Users.CountAsync(u => u.ParentId == userId),

            ReportsGenerated = await _db.Designs.CountAsync(),

            TodayProspects = await _db.Prospects.CountAsync(p => p.CreatedAt.Date == DateTime.UtcNow.Date),

            MyProspects = await _db.Prospects.CountAsync(p => p.OwnerId == userId)
        };

        return stats;
    }
}