using Microsoft.EntityFrameworkCore;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Domain.Models.Reports;
using HumanDesign.Application.Interfaces;

namespace HumanDesign.Application.Services.Reference;
public class ContentResolverService(AppDbContext db) : IContentResolverService
{
    private readonly AppDbContext _db = db;

    // =========================
    // CORE HELPER
    // =========================

    private static string PickLevel(string? preview, string? summary, string? detail)
        => detail ?? summary ?? preview ?? "";

    private static ResolvedContent Map(
        string key,
        string title,
        string? preview,
        string? summary,
        string? detail,
        int? img)
        => new()
        {
            Key = key,
            Title = title,
            Description = PickLevel(preview, summary, detail),
            ImageId = img
        };

    // =========================
    // TYPE BUNDLE
    // =========================

    public async Task<TypeBundleResult> ResolveTypeBundleAsync(string typeName)
    {
        var type = await _db.Types
            .FirstAsync(t => t.Name == typeName);

        var strategy = await _db.Attributes.FindAsync(type.Strategy);
        var signature = await _db.Attributes.FindAsync(type.Characteristic);
        var notSelf = await _db.Attributes.FindAsync(type.Theme);

        return new TypeBundleResult
        {
            Type = Map(type.Name, type.Name, type.Preview, type.Summary, type.Detail, type.FileId),

            Strategy = Map("Strategy", strategy!.Title,
                strategy.Preview, strategy.Summary, strategy.Detail, strategy.ImgNo),

            Signature = Map("Signature", signature!.Title,
                signature.Preview, signature.Summary, signature.Detail, signature.ImgNo),

            NotSelf = Map("NotSelf", notSelf!.Title,
                notSelf.Preview, notSelf.Summary, notSelf.Detail, notSelf.ImgNo)
        };
    }

    // =========================
    // PROFILE
    // =========================

    public async Task<ResolvedContent?> ResolveProfileAsync(string profile)
    {
        var parts = profile.Split('/');
        var p1 = int.Parse(parts[0]);
        var p2 = int.Parse(parts[1]);

        var entity = await _db.Profiles
            .FirstOrDefaultAsync(p => p.Code1 == p1 && p.Code2 == p2);

        if (entity == null) return null;

        return Map(profile, entity.Name,
            null, entity.Summary, entity.Detail, entity.FileId);
    }

    // =========================
    // CROSS
    // =========================

    public async Task<ResolvedContent?> ResolveCrossAsync(string crossName)
    {
        var entity = await _db.Crosses
            .FirstOrDefaultAsync(c => c.Name == crossName);

        if (entity == null) return null;

        return new ResolvedContent
        {
            Key = crossName,
            Title = entity.Name,
            Description = $"{entity.Type} Cross",
            ImageId = null
        };
    }

    // =========================
    // ATTRIBUTE (Authority, Variables, Definition)
    // =========================

    public async Task<ResolvedContent?> ResolveAttributeAsync(string property, string value)
    {
        var entity = await _db.Attributes
            .FirstOrDefaultAsync(a => a.Property == property && a.Value == value);

        if (entity == null) return null;

        return Map(value, entity.Title,
            entity.Preview, entity.Summary, entity.Detail, entity.ImgNo);
    }

    // =========================
    // CENTER
    // =========================

    public async Task<ResolvedContent?> ResolveCenterAsync(string centerName, string definition)
    {
        var entity = await _db.Centers
            .FirstOrDefaultAsync(c => c.CenterName == centerName && c.Definition == definition);

        if (entity == null) return null;

        return Map(entity.CenterName, entity.CenterName,
            entity.Preview, entity.Summary, entity.Detail, entity.FileId);
    }

    // =========================
    // GATE
    // =========================

    public async Task<ResolvedContent?> ResolveGateAsync(int gate)
    {
        var entity = await _db.Gates.FindAsync(gate);
        if (entity == null) return null;

        return Map($"Gate{gate}", entity.Title,
            entity.Preview, entity.Summary, entity.Detail, entity.FileId);
    }

    // =========================
    // CHANNEL
    // =========================

    public async Task<ResolvedContent?> ResolveChannelAsync(int channelId)
    {
        var entity = await _db.Channels.FindAsync(channelId);
        if (entity == null) return null;

        return Map($"Channel{channelId}", entity.Name,
            entity.Preview, entity.Summary, entity.Detail, entity.FileId);
    }
}