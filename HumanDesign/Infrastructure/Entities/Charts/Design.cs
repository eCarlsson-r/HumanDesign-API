using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanDesign.Infrastructure.Entities.Reference;

namespace HumanDesign.Infrastructure.Entities.Charts;

[Table("design")]
public class Design
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("name")]
    public Guid ProspectId { get; set; }

    [ForeignKey(nameof(ProspectId))]
    public Prospect Prospect { get; set; } = default!;
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
    public int IncarnationCross { get; set; } = default!;

    // Navigation
    public ICollection<CenterEntity> Centers { get; set; } = new List<CenterEntity>();
    public ICollection<PlanetaryActivation> Activations { get; set; } = new List<PlanetaryActivation>();
    public ICollection<ChannelEntity> Channels { get; set; } = new List<ChannelEntity>();
    public VariableSet Variables { get; set; } = default!;
}