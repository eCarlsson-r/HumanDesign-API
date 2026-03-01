namespace HumanDesign.Domain.Models.Charts;
public class CenterState
{
    public string Name { get; set; } = "";
    public bool IsDefined { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? FileId { get; set; }
}