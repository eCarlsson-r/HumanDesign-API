using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("design")]
public class Design
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("name")]
    public string Name { get; set; } = default!;
    [Column("birth_date_utc")]
    public DateTime BirthDateUtc { get; set; }
    [Column("latitude")]
    public double Latitude { get; set; }
    [Column("longitude")]
    public double Longitude { get; set; }
    [Column("time_zone")]
    public string TimeZone { get; set; } = default!;

    // Core HD outputs
    [Column("type")]
    public string Type { get; set; } = default!;
    [Column("authority")]
    public string Authority { get; set; } = default!;
    [Column("definition")]
    public string Definition { get; set; } = default!;
    [Column("profile")]
    public string Profile { get; set; } = default!;
    [Column("incarnation_cross")]
    public string IncarnationCross { get; set; } = default!;

    // Navigation
    public ICollection<PlanetaryActivation> Activations { get; set; } = new List<PlanetaryActivation>();
    public ICollection<DefinedChannel> Channels { get; set; } = new List<DefinedChannel>();
    public VariableSet Variables { get; set; } = default!;
}