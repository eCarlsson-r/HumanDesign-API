namespace HumanDesign.Domain.Models.Diagram;

public class HumanDesignDiagramModel
{
    public List<int> ActiveGates { get; set; } = new();
    public List<string> ActiveChannels { get; set; } = new();
    public List<string> DefinedCenters { get; set; } = new();
}