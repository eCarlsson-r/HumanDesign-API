using HumanDesign.Domain.Models.Reference;

namespace HumanDesign.Domain.Models.Reports;
public class SummaryReport
{
    public PreviewReport Preview { get; set; } = default!;

    public AttributeDetail Type { get; set; } = default!;
    public AttributeDetail Profile { get; set; } = default!;
    public AttributeDetail Definition { get; set; } = default!;
    public AttributeDetail Cross { get; set; } = default!;

    public List<AttributeDetail> Channels { get; set; } = new();
}