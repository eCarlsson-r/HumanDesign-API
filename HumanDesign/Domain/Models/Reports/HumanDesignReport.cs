namespace HumanDesign.Domain.Models.Reports;

public class HumanDesignReport
{
    public string Level { get; set; } = default!;

    // CORE TYPE INFO

    public ResolvedContent? Type { get; set; }
    public ResolvedContent? Strategy { get; set; }
    public ResolvedContent? Signature { get; set; }
    public ResolvedContent? NotSelfTheme { get; set; }

    // NEW — converted from string → resolved

    public ResolvedContent? Authority { get; set; }
    public ResolvedContent? Definition { get; set; }

    // PROFILE + CROSS

    public ResolvedContent? Profile { get; set; }
    public ResolvedContent? Cross { get; set; }

    // VARIABLES

    public Dictionary<string, ResolvedContent> Variables { get; set; } = new();

    // CENTERS

    public List<CenterReport> Centers { get; set; } = new();

    // DETAIL LEVEL

    public List<ResolvedContent> Gates { get; set; } = new();
    public List<ResolvedContent> Channels { get; set; } = new();
}