public class DesignResult
{
    public ChannelResult Channels { get; set; } = default!;
    public VariableResult Variables { get; set; } = default!;
    public IEnumerable<PlanetaryActivation> Activations { get; set; } = new List<PlanetaryActivation>();
}