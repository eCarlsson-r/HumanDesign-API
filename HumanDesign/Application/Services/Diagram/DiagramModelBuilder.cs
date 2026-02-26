using HumanDesign.Infrastructure.Data;
using HumanDesign.Domain.Models.Diagram;
using Microsoft.EntityFrameworkCore;

namespace HumanDesign.Application.Services.Diagram;

public class DiagramModelBuilder
{
    private readonly AppDbContext _db;

    public DiagramModelBuilder(AppDbContext db)
    {
        _db = db;
    }

    public async Task<HumanDesignDiagramModel> BuildAsync(Guid designId)
    {
        var design = await _db.Designs
            .Include(d => d.Channels)
            .Include(d => d.Activations)
            .Include(d => d.Centers)
            .FirstAsync(d => d.Id == designId);

        return new HumanDesignDiagramModel
        {
            ActiveGates = design.Activations.Select(a => a.Gate).Distinct().ToList(),

            ActiveChannels = design.Channels.Select(c => c.Id.ToString()).ToList(),

            DefinedCenters = design.Centers
                .Where(c => c.Definition == "defined")
                .Select(c => c.CenterName)
                .ToList()
        };
    }
}