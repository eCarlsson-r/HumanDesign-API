namespace HumanDesign.Domain.Models.Charts;
public class HumanDesignChart
{
    public Guid DesignId { get; set; }

    // BASIC INFO
    public string Type { get; set; } = "";
    public string Authority { get; set; } = "";
    public string Definition { get; set; } = "";
    public string Profile { get; set; } = "";
    public string IncarnationCross { get; set; } = "";

    // STRUCTURE
    public List<CenterState> Centers { get; set; } = new();
    public List<ChannelState> Channels { get; set; } = new();

    // PLANETARY ACTIVATIONS
    public List<GateActivation> ConsciousGates { get; set; } = new();
    public List<GateActivation> UnconsciousGates { get; set; } = new();

    // VARIABLES
    public VariableState Variables { get; set; } = new();
}