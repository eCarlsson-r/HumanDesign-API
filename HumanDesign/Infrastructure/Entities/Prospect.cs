using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanDesign.Infrastructure.Entities.Charts;

namespace HumanDesign.Infrastructure.Entities;

[Table("prospects")]
public class Prospect
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string FullName { get; set; } = default!;

    [Required]
    public DateTime BirthDateLocal { get; set; }
    public DateTime BirthDateUtc { get; set; }

    [Required]
    public string BirthLocation { get; set; } = default!;

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public string TimeZone { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Design? GeneratedDesign { get; set; }
}