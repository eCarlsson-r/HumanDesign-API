using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanDesign.Domain.Enums;
using HumanDesign.Infrastructure.Entities.Charts;

namespace HumanDesign.Infrastructure.Entities;

[Table("prospects")]
public class Prospect
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";

    public Guid OwnerId { get; set; }
    public UserEntity Owner { get; set; } = null!;

    [Required]
    public DateTime BirthDateLocal { get; set; }
    public DateTime BirthDateUtc { get; set; }

    [Required]
    public string BirthLocation { get; set; } = default!;

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public string TimeZone { get; set; } = default!;

    // Navigation
    public Design? GeneratedDesign { get; set; }
    public ProspectStatus Status { get; set; } = ProspectStatus.Preview;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}