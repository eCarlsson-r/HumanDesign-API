using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public string IncarnationCross { get; set; } = default!;

    // Navigation
    public ICollection<CenterDefinition> CenterDefinitions { get; set; } = [];
    public ICollection<PlanetaryActivation> Activations { get; set; } = [];
    public ICollection<DefinedChannel> Channels { get; set; } = [];
    public VariableSet Variables { get; set; } = default!;
}