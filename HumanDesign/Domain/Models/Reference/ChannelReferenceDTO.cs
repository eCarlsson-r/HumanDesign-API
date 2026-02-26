namespace HumanDesign.Domain.Models.Reference;
public class ChannelInterpretationResult
{
    public int GateA { get; set; }
    public int GateB { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}