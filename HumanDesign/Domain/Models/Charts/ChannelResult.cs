namespace HumanDesign.Models;
public class ChannelResult
{
    public List<int> DefinedChannels { get; set; } = new();
    public List<string> DefinedCenters { get; set; } = new();
    public string Type { get; set; } = default!;
    public string Authority { get; set; } = default!;
}