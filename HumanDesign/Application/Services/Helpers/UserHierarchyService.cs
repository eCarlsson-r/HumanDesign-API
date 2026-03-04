using HumanDesign.Application.Interfaces;
using HumanDesign.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HumanDesign.Application.Services.Helpers;
public class UserHierarchyService(AppDbContext db) : IUserHierarchyService
{
    private readonly AppDbContext _db = db;

    public async Task<List<Guid>> GetDescendantUserIdsAsync(Guid userId)
    {
        var result = new List<Guid>();
        await LoadChildren(userId, result);
        return result;
    }

    private async Task LoadChildren(Guid parentId, List<Guid> result)
    {
        var children = await _db.Users
            .Where(u => u.ParentId == parentId)
            .Select(u => u.Id).ToListAsync();

        foreach (var child in children)
        {
            result.Add(child);
            await LoadChildren(child, result);
        }
    }
}