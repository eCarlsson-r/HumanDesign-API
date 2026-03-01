using Microsoft.EntityFrameworkCore;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Application.Interfaces;
using HumanDesign.Domain.Models.Reports;
using HumanDesign.Infrastructure.Entities.Charts;

namespace HumanDesign.Application.Services.Reports;

public class HumanDesignReportBuilder(
    AppDbContext db,
    IContentResolverService resolver) : IHumanDesignReportBuilder
{
    private readonly AppDbContext _db = db;
    private readonly IContentResolverService _resolver = resolver;

    // ================= PREVIEW =================

    public async Task<HumanDesignReport> BuildPreviewAsync(Guid designId)
    {
        var design = await LoadDesignAsync(designId);

        var report = new HumanDesignReport
        {
            Level = "Preview"
        };

        // TYPE BUNDLE (1 resolver call)

        var typeBundle = await _resolver.ResolveTypeBundleAsync(design.Type);

        report.Type = typeBundle.Type;
        report.Strategy = typeBundle.Strategy;
        report.Signature = typeBundle.Signature;
        report.NotSelfTheme = typeBundle.NotSelf;

        // PROFILE

        report.Profile = await _resolver.ResolveProfileAsync(design.Profile);

        // AUTHORITY + DEFINITION (NEW)

        report.Authority = await _resolver.ResolveAttributeAsync("Authority", design.Authority);
        report.Definition = await _resolver.ResolveAttributeAsync("Definition", design.Definition);

        // INCARNATION CROSS

        report.Cross = await _resolver.ResolveCrossAsync(design.IncarnationCross);

        // CENTERS WITH CONTENT

        report.Centers = await ResolveCentersAsync(designId);

        // VARIABLES

        report.Variables = await ResolveVariablesAsync(design);

        report.Gates = await ResolveGatesAsync(design);
        report.Channels = await ResolveChannelsAsync(design);

        return report;
    }

    // ================= SUMMARY =================

    public async Task<HumanDesignReport> BuildSummaryAsync(Guid designId)
    {
        var report = await BuildPreviewAsync(designId);
        var design = await LoadDesignAsync(designId);

        report.Level = "Summary";

        // INCARNATION CROSS

        report.Cross = await _resolver.ResolveCrossAsync(design.IncarnationCross);

        // CENTERS WITH CONTENT

        report.Centers = await ResolveCentersAsync(designId);

        // VARIABLES

        report.Variables = await ResolveVariablesAsync(design);

        return report;
    }

    // ================= DETAIL =================

    public async Task<HumanDesignReport> BuildDetailAsync(Guid designId)
    {
        var report = await BuildSummaryAsync(designId);
        var design = await LoadDesignAsync(designId);

        report.Level = "Detail";

        report.Gates = await ResolveGatesAsync(design);
        report.Channels = await ResolveChannelsAsync(design);

        return report;
    }

    // =====================================================
    // INTERNAL HELPERS
    // =====================================================

    private async Task<Design> LoadDesignAsync(Guid id)
    {
        return await _db.Designs
            .Include(d => d.Channels)
            .Include(d => d.Activations)
            .Include(d => d.Variables)
            .Include(d => d.CenterDefinitions)
            .FirstOrDefaultAsync(d => d.Id == id)
            ?? throw new Exception("Design not found");
    }

    // ---------------- CENTERS ----------------

    private async Task<List<CenterReport>> ResolveCentersAsync(Guid designId)
    {
        var centers = await _db.CenterDefinitions
            .Where(c => c.DesignId == designId)
            .ToListAsync();

        var result = new List<CenterReport>();

        foreach (var c in centers)
        {
            var content = await _resolver.ResolveCenterAsync(c.CenterName, c.Definition);

            result.Add(new CenterReport
            {
                Name = c.CenterName,
                IsDefined = c.Definition == "Defined",
                Content = content
            });
        }

        return result;
    }

    // ---------------- VARIABLES ----------------

    private async Task<Dictionary<string, ResolvedContent>> ResolveVariablesAsync(Design design)
    {
        var result = new Dictionary<string, ResolvedContent>();

        var vars = new Dictionary<string, string?>
        {
            ["Digestion"] = design.Variables.Digestion,
            ["Cognition"] = design.Variables.Cognition,
            ["Reasoning"] = design.Variables.Reasoning,
            ["Motivation"] = design.Variables.Motivation,
            ["Perspective"] = design.Variables.Perspective,
            ["Environment"] = design.Variables.Environment
        };

        foreach (var kv in vars)
        {
            if (kv.Value == null) continue;

            var content = await _resolver.ResolveAttributeAsync(kv.Key, kv.Value);
            if (content != null) result[kv.Key] = content;
        }

        return result;
    }

    // ---------------- GATES ----------------

    private async Task<List<ResolvedContent>> ResolveGatesAsync(Design design)
    {
        var list = new List<ResolvedContent>();

        var gates = design.Activations
            .Select(a => a.Gate)
            .Distinct();

        foreach (var g in gates)
        {
            var content = await _resolver.ResolveGateAsync(g);
            if (content != null) list.Add(content);
        }

        return list;
    }

    // ---------------- CHANNELS ----------------

    private async Task<List<ResolvedContent>> ResolveChannelsAsync(Design design)
    {
        var list = new List<ResolvedContent>();

        foreach (var ch in design.Channels)
        {
            var content = await _resolver.ResolveChannelAsync(ch.Id);
            if (content != null) list.Add(content);
        }

        return list;
    }
}