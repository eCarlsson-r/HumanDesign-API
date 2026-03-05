using HumanDesign.Domain.Models.Charts;
using HumanDesign.Infrastructure.Entities.Charts;

namespace HumanDesign.Domain.Models.Reports;

public class HumanDesignReport
{
    public string Level { get; set; } = default!;
    public ResolvedContent? Type { get; set; }
    public ResolvedContent? Strategy { get; set; }
    public ResolvedContent? Signature { get; set; }
    public ResolvedContent? NotSelfTheme { get; set; }
    public ResolvedContent? Authority { get; set; }
    public ResolvedContent? Definition { get; set; }
    public ResolvedContent? Profile { get; set; }
    public ResolvedContent? Cross { get; set; }
    public Dictionary<string, ResolvedContent> Variables { get; set; } = [];
    public Dictionary<string, VariableArrow> Arrows { get; set; } = [];
    public List<CenterReport> Centers { get; set; } = [];
    public List<GateReport> Gates { get; set; } = [];

    public List<GateActivation> DesignGates { get; set; } = [];
    public List<GateActivation> PersonalityGates { get; set; } = [];
    public List<ResolvedContent> Channels { get; set; } = [];
}