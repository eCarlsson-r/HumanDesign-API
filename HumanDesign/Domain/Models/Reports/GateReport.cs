namespace HumanDesign.Domain.Models.Reports;
public class GateReport
{
    public int Gate { get; set; }
    public string Type { get; set; } = "";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}