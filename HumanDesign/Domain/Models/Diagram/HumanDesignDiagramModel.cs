using HumanDesign.Infrastructure.Entities.Charts;

namespace HumanDesign.Domain.Models.Diagram;

public class HumanDesignDiagramModel
{
    public List<PlanetaryActivation> ActiveGates { get; set; } = new();
    public List<DefinedChannel> ActiveChannels { get; set; } = new();
    public List<CenterDefinition> DefinedCenters { get; set; } = new();
    public VariableSet VariableArrows { get; set; } = new();
}