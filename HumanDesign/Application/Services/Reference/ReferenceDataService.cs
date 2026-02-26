using Microsoft.EntityFrameworkCore;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Application.Interfaces;
using HumanDesign.Domain.Models.Reference;

namespace HumanDesign.Application.Services.Reference;

public class ReferenceDataService : IReferenceDataService
{
    private readonly AppDbContext _db;

    private ReferenceDataBundle? _cache;
    private readonly SemaphoreSlim _lock = new(1, 1);

    public ReferenceDataService(AppDbContext db)
    {
        _db = db;
    }

    // ================= MAIN ENTRY =================

    public async Task<ReferenceDataBundle> GetBundleAsync()
    {
        if (_cache != null)
            return _cache;

        await _lock.WaitAsync();

        try
        {
            if (_cache != null)
                return _cache;

            _cache = await LoadBundleAsync();
            return _cache;
        }
        finally
        {
            _lock.Release();
        }
    }

    // ================= LOADER =================

    private async Task<ReferenceDataBundle> LoadBundleAsync()
    {
        var bundle = new ReferenceDataBundle();

        // TYPES
        bundle.Types = await _db.Types
            .AsNoTracking()
            .ToDictionaryAsync(
                t => t.Name,
                t => new AttributeDetail
                {
                    Title = t.Name,
                    Preview = t.Preview,
                    Summary = t.Summary,
                    Detail = t.Detail,
                    ImageUrl = null
                });

        // GATES
        bundle.Gates = await _db.Gates
            .AsNoTracking()
            .ToDictionaryAsync(
                g => g.Number,
                g => new AttributeDetail
                {
                    Title = g.Title,
                    Preview = g.Preview,
                    Summary = g.Summary,
                    Detail = g.Detail,
                    ImageUrl = null
                });

        // CHANNELS
        bundle.Channels = await _db.Channels
            .AsNoTracking()
            .ToDictionaryAsync(
                c => c.Id,
                c => new AttributeDetail
                {
                    Title = c.Name,
                    Preview = c.Preview,
                    Summary = c.Summary,
                    Detail = c.Detail,
                    ImageUrl = null
                }
            );

        // PROFILES
        bundle.Profiles = await _db.Profiles
            .AsNoTracking()
            .ToDictionaryAsync(
                p => p.Code1 + "/" + p.Code2,
                p => new AttributeDetail
                {
                    Title = p.Name,
                    Preview = null,
                    Summary = p.Summary,
                    Detail = p.Detail,
                    ImageUrl = null
                });

        // CROSSES
        bundle.Crosses = await _db.Crosses
            .AsNoTracking()
            .ToDictionaryAsync(
                c => c.Id,
                c => new AttributeDetail
                {
                    Title = c.Name,
                    Summary = c.Type,
                    Detail = null,
                    ImageUrl = null
                });

        // VARIABLES
        bundle.Variables = await _db.Attributes
            .AsNoTracking()
            .ToDictionaryAsync(
                a => $"{a.Property}:{a.Value}",
                a => new AttributeDetail
                {
                    Title = a.Value,
                    Preview = a.Preview,
                    Summary = a.Summary,
                    Detail = a.Detail,
                    ImageUrl = null
                });

        return bundle;
    }

    // ================= LOOKUPS =================

    public async Task<AttributeDetail?> GetTypeAsync(string type)
    {
        var bundle = await GetBundleAsync();
        bundle.Types.TryGetValue(type, out var result);
        return result;
    }

    public async Task<AttributeDetail?> GetGateAsync(int gate)
    {
        var bundle = await GetBundleAsync();
        bundle.Gates.TryGetValue(gate, out var result);
        return result;
    }

    public async Task<AttributeDetail?> GetChannelAsync(int channelId)
    {
        var bundle = await GetBundleAsync();
        bundle.Channels.TryGetValue(channelId, out var result);
        return result;
    }

    public async Task<AttributeDetail?> GetProfileAsync(string profile)
    {
        var bundle = await GetBundleAsync();
        bundle.Profiles.TryGetValue(profile, out var result);
        return result;
    }

    public async Task<AttributeDetail?> GetCrossAsync(int cross)
    {
        var bundle = await GetBundleAsync();
        bundle.Crosses.TryGetValue(cross, out var result);
        return result;
    }

    public async Task<AttributeDetail?> GetVariableAsync(string variableKey)
    {
        var bundle = await GetBundleAsync();
        bundle.Variables.TryGetValue(variableKey, out var result);
        return result;
    }
}