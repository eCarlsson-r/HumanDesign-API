using Microsoft.EntityFrameworkCore;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Infrastructure.Entities.Charts;
using HumanDesign.Application.Interfaces;
using HumanDesign.Domain.Models.Charts;
using HumanDesign.Domain.Models.Reference;
using HumanDesign.Domain.Models.Reports;
using HumanDesign.Application.Services.Diagram;

namespace HumanDesign.Application.Services.Reports;

public class HumanDesignReportBuilder : IHumanDesignReportBuilder
{
    private readonly AppDbContext _db;
    private readonly IReferenceDataService _reference;
    private readonly DiagramModelBuilder _diagramBuilder;
    private readonly IDiagramRenderer _renderer;

    public HumanDesignReportBuilder(
        AppDbContext db,
        IReferenceDataService reference,
        DiagramModelBuilder diagramBuilder,
        IDiagramRenderer renderer)
    {
        _db = db;
        _reference = reference;
        _diagramBuilder = diagramBuilder;
        _renderer = renderer;
    }

    // ================= PREVIEW =================

    public async Task<HumanDesignReport> BuildPreviewAsync(Guid designId)
    {
        var design = await LoadDesignAsync(designId);

        var report = new HumanDesignReport
        {
            Level = "Preview"
        };

        report.Type = await _reference.GetTypeAsync(design.Type);
        report.Authority = design.Authority;
        report.Profile = await _reference.GetProfileAsync(design.Profile);

        report.DiagramSvg = await GenerateDiagramAsync(design);

        return report;
    }

    // ================= SUMMARY =================

    public async Task<HumanDesignReport> BuildSummaryAsync(Guid designId)
    {
        var report = await BuildPreviewAsync(designId);

        var design = await LoadDesignAsync(designId);

        report.Level = "Summary";
        report.Definition = design.Definition;
        report.Cross = await _reference.GetCrossAsync(design.IncarnationCross);

        report.Centers = await GetCentersAsync(designId);

        report.Variables = await LoadVariablesAsync(design);

        return report;
    }

    // ================= DETAIL =================

    public async Task<HumanDesignReport> BuildDetailAsync(Guid designId)
    {
        var report = await BuildSummaryAsync(designId);

        var design = await LoadDesignAsync(designId);

        report.Level = "Detail";

        report.Gates = await LoadGateInterpretations(design);
        report.Channels = await LoadChannelInterpretations(design);

        return report;
    }

    // ====================================================
    // INTERNAL HELPERS
    // ====================================================

    private async Task<Design> LoadDesignAsync(Guid id)
    {
        var design = await _db.Designs
            .Include(d => d.Channels)
            .Include(d => d.Activations)
            .Include(d => d.Variables)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (design == null)
            throw new Exception("Design not found");

        return design;
    }

    private async Task<Dictionary<string, AttributeDetail>> LoadVariablesAsync(Design design)
    {
        var result = new Dictionary<string, AttributeDetail>();

        var vars = new Dictionary<string, string?>
        {
            ["Digestion"] = design.Variables.Digestion,
            ["Cognition"] = design.Variables.Cognition,
            ["Motivation"] = design.Variables.Motivation,
            ["Perspective"] = design.Variables.Perspective,
            ["Environment"] = design.Variables.Environment
        };

        foreach (var kv in vars)
        {
            if (kv.Value == null) continue;

            var detail = await _reference.GetVariableAsync($"{kv.Key}:{kv.Value}");
            if (detail != null)
                result[kv.Key] = detail;
        }

        return result;
    }

    private async Task<List<AttributeDetail>> LoadGateInterpretations(Design design)
    {
        var list = new List<AttributeDetail>();

        var gates = design.Activations
            .Select(a => a.Gate)
            .Distinct();

        foreach (var gate in gates)
        {
            var detail = await _reference.GetGateAsync(gate);
            if (detail != null)
                list.Add(detail);
        }

        return list;
    }

    private async Task<List<AttributeDetail>> LoadChannelInterpretations(Design design)
    {
        var list = new List<AttributeDetail>();

        foreach (var ch in design.Channels)
        {
            var detail = await _reference.GetChannelAsync(ch.Id);
            if (detail != null)
                list.Add(detail);
        }

        return list;
    }

    private async Task<List<CenterState>> GetCentersAsync(Guid designId)
    {
        return await _db.CenterDefinitions
            .Where(c => c.DesignId == designId)
            .Select(c => new CenterState
            {
                Name = c.CenterName,
                IsDefined = c.Definition == "defined"
            })
            .ToListAsync();
    }

    // ====================================================
    // DIAGRAM GENERATION
    // ====================================================

    private async Task<string> GenerateDiagramAsync(Design design)
    {
        var model = await _diagramBuilder.BuildAsync(design.Id);
        return await _renderer.RenderSvgAsync(model);
    }
}