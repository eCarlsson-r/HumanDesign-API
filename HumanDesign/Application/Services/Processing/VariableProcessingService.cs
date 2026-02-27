using HumanDesign.Infrastructure.Data;
using HumanDesign.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HumanDesign.Application.Services.Processing;
public class VariableProcessingService : IVariableProcessingService
{
    private readonly AppDbContext _db;

    public VariableProcessingService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Dictionary<string, string>> ProcessVariablesAsync(Guid designId)
    {
        var design = await _db.Designs
            .Include(d => d.Variables)
            .FirstOrDefaultAsync(d => d.Id == designId);

        if (design == null) return new();

        return new Dictionary<string, string>
        {
            ["Digestion"] = design.Variables.Digestion ?? "",
            ["Reasoning"] = design.Variables.Reasoning ?? "",
            ["Cognition"] = design.Variables.Cognition ?? "",
            ["Motivation"] = design.Variables.Motivation ?? "",
            ["Perspective"] = design.Variables.Perspective ?? "",
            ["Environment"] = design.Variables.Environment ?? ""
        };
    }
}