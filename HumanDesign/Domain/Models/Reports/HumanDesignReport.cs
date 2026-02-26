using HumanDesign.Domain.Models.Charts;
using HumanDesign.Domain.Models.Reference;

namespace HumanDesign.Domain.Models.Reports;

public class HumanDesignReport
{
    public string Level { get; set; } = default!;

    public AttributeDetail? Type { get; set; }
    public string Authority { get; set; } = "";
    public string Definition { get; set; } = "";

    public AttributeDetail? Profile { get; set; }
    public AttributeDetail? Cross { get; set; }

    public Dictionary<string, AttributeDetail> Variables { get; set; } = new();

    public List<CenterState> Centers { get; set; } = new();

    public List<AttributeDetail> Gates { get; set; } = new();
    public List<AttributeDetail> Channels { get; set; } = new();

    public string? DiagramSvg { get; set; }
}