namespace HumanDesign.Domain.Models.Requests;

public class CreateProspectRequest
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;

    public DateTime BirthDate { get; set; }

    public TimeSpan BirthTime { get; set; }

    public string BirthLocation { get; set; } = default!;
}