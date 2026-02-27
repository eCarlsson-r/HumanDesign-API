namespace HumanDesign.Domain.Models.Reports;
public class PreviewReport
{
    public string Name { get; set; } = default!;

    public string Type { get; set; } = default!;
    public string Authority { get; set; } = default!;
    public string Profile { get; set; } = default!;
    public string Definition { get; set; } = default!;
    public string IncarnationCross { get; set; } = default!;

    public string DiagramSvg { get; set; } = default!;
}