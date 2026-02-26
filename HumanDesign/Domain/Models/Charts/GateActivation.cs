namespace HumanDesign.Domain.Models.Charts;
public class GateActivation
{
    public int Gate { get; set; }
    public int Line { get; set; }
    public string Planet { get; set; } = "";
    public string Center { get; set; } = "";
    public string Quarter { get; set; } = "";
}