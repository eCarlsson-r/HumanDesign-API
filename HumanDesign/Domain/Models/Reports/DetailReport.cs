using HumanDesign.Domain.Models.Charts;
using HumanDesign.Domain.Models.Reference;

namespace HumanDesign.Domain.Models.Reports;
public class DetailReport
{
    public SummaryReport Summary { get; set; } = default!;

    public List<AttributeDetail> Gates { get; set; } = new();
    public List<CenterState> Centers { get; set; } = new();

    public Dictionary<string, string> Variables { get; set; } = new();
}