using Microsoft.EntityFrameworkCore;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Application.Interfaces;
using HumanDesign.Domain.Models.Reports;
using HumanDesign.Infrastructure.Entities.Charts;
using HumanDesign.Domain.Models.Charts;

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
        return await GenerateReport(designId, "Preview");
    }

    // ================= SUMMARY =================

    public async Task<HumanDesignReport> BuildSummaryAsync(Guid designId)
    {
        return await GenerateReport(designId, "Summary");
    }

    // ================= DETAIL =================

    public async Task<HumanDesignReport> BuildDetailAsync(Guid designId)
    {
        return await GenerateReport(designId, "Detail");
    }

    // =====================================================
    // INTERNAL HELPERS
    // =====================================================

    private async Task<HumanDesignReport> GenerateReport(Guid id, string level)
    {
        var design = await _db.Designs
            .Include(d => d.Channels)
            .Include(d => d.Activations)
            .Include(d => d.Variables)
            .Include(d => d.Variables.DigestionArrow)
            .Include(d => d.Variables.EnvironmentArrow)
            .Include(d => d.Variables.PerspectiveArrow)
            .Include(d => d.Variables.AwarenessArrow)
            .Include(d => d.CenterDefinitions)
            .FirstOrDefaultAsync(d => d.Id == id);

        design ??= await _db.Designs
            .Include(d => d.Channels)
            .Include(d => d.Activations)
            .Include(d => d.Variables)
            .Include(d => d.Variables.DigestionArrow)
            .Include(d => d.Variables.EnvironmentArrow)
            .Include(d => d.Variables.PerspectiveArrow)
            .Include(d => d.Variables.AwarenessArrow)
            .Include(d => d.CenterDefinitions)
            .FirstOrDefaultAsync(d => d.ProspectId == id) ?? throw new Exception("Design not found");

        var typeBundle = await _resolver.ResolveTypeBundleAsync(design.Type, level);
        var report = new HumanDesignReport
        {
            Level = level,
            Type = typeBundle.Type,
            Strategy = typeBundle.Strategy,
            Signature = typeBundle.Signature,
            NotSelfTheme = typeBundle.NotSelf,
            Authority = await _resolver.ResolveAttributeAsync("Authority", design.Authority, level),
            Definition = await _resolver.ResolveAttributeAsync("Definition", design.Definition, level),
            Profile = await _resolver.ResolveProfileAsync(design.Profile, level),
            Cross = await _resolver.ResolveCrossAsync(design.IncarnationCross, level),
            Centers = await ResolveCentersAsync(design, level),
            Variables = await ResolveVariablesAsync(design, level),
            Arrows = await ResolveArrowsAsync(design),
            Gates = await ResolveGatesAsync(design, level),
            DesignGates = await ResolveGateList(design, "Design"),
            PersonalityGates = await ResolveGateList(design, "Personality"),
            Channels = await ResolveChannelsAsync(design, level)
        };
        
        return report;
    }

    // ---------------- CENTERS ----------------

    private async Task<List<CenterReport>> ResolveCentersAsync(Design design, string level)
    {
        var centers = design.CenterDefinitions.ToList();

        var result = new List<CenterReport>();

        foreach (var c in centers)
        {
            var content = await _resolver.ResolveCenterAsync(c.CenterName, c.Definition, level);

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

    private async Task<Dictionary<string, ResolvedContent>> ResolveVariablesAsync(Design design, string level)
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

            var content = await _resolver.ResolveAttributeAsync(kv.Key, kv.Value, level);
            if (content != null) result[kv.Key] = content;
        }

        return result;
    }

    // ---------------- GATES ----------------

    private async Task<List<GateReport>> ResolveGatesAsync(Design design, string level)
    {
        var list = new List<GateReport>();

        var gates = design.Activations
            .Select(a => a.Gate)
            .Distinct();

        var gateList = design.Activations
            .Select(a => new{a.Gate, a.Type})
            .Distinct();

        foreach (var g in gates)
        {
            var gateTypes = gateList.Where(a => a.Gate == g).ToArray();
            var type = (gateTypes.Length > 1) ? "Both" : gateTypes[0].Type;
            var content = await _resolver.ResolveGateAsync(g, level);
            if (content != null) list.Add(new GateReport
            {
                Gate = g,
                Type = type,
                Title = content.Title,
                Description = content.Description
            });
        }

        return list;
    }

    // ---------------- CHANNELS ----------------

    private async Task<List<ResolvedContent>> ResolveChannelsAsync(Design design, string level)
    {
        var list = new List<ResolvedContent>();

        foreach (var ch in design.Channels)
        {
            var content = await _resolver.ResolveChannelAsync(ch.Id, level);
            if (content != null) list.Add(content);
        }

        return list;
    }

    private static async Task<Dictionary<string, VariableArrow>> ResolveArrowsAsync(Design design)
    {
        Console.WriteLine("Variables : " + design.Variables);
        return new Dictionary<string, VariableArrow>
        {
            ["Digestion"] = design.Variables.DigestionArrow,
            ["Environment"] = design.Variables.EnvironmentArrow,
            ["Awareness"] = design.Variables.AwarenessArrow,
            ["Perspective"] = design.Variables.PerspectiveArrow
        };
    }

    private static async Task<List<GateActivation>> ResolveGateList(Design design, string type)
    {
        var list = new List<GateActivation>();
        var gates = design.Activations.Where(a => a.Type == type).ToList();
        foreach (var gate in gates)
        {
            list.Add(new GateActivation
            {
                Planet = gate.Planet,
                Gate = gate.Gate,
                Type = gate.Type,
                Line = gate.Line,
                FixingState = gate.FixingState
            });
        }

        return list;
    }
  
}