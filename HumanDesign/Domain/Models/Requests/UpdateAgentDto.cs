namespace HumanDesign.Domain.Models.Requests;
public class UpdateAgentDto
{
    public string FullName { get; set; } = "";
    public string Role { get; set; } = "";
    public Guid? ParentId { get; set; }
}