namespace HumanDesign.Domain.Models.Reports;
public class CenterReport
{
    public string Name { get; set; } = "";

    public bool IsDefined { get; set; }

    public ResolvedContent? Content { get; set; }
}