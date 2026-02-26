namespace HumanDesign.Domain.Models.Reference;

public class ReferenceDataBundle
{
    public Dictionary<string, AttributeDetail> Types { get; set; } = new();

    public Dictionary<int, AttributeDetail> Gates { get; set; } = new();

    public Dictionary<int, AttributeDetail> Channels { get; set; } = new();

    public Dictionary<string, AttributeDetail> Profiles { get; set; } = new();

    public Dictionary<int, AttributeDetail> Crosses { get; set; } = new();

    public Dictionary<string, AttributeDetail> Variables { get; set; } = new();
}