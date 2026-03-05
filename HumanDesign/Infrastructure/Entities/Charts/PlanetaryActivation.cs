using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities.Charts;

[Table("planetary_activations")]
public class PlanetaryActivation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("design_id")]
    public Guid DesignId { get; set; }
    [Column("type")]
    public string Type { get; set; } = default!;
    [Column("planet")]
    public string Planet { get; set; } = default!;
    [Column("gate")]
    public int Gate { get; set; }
    [Column("line")]
    public int Line { get; set; }
    [Column("color")]
    public int Color { get; set; }
    [Column("tone")]
    public int Tone { get; set; }
    [Column("base")]
    public int Base { get; set; }
    [Column("fixation")]
    public string FixingState { get; set; } = default!;

    public Design Design { get; set; } = default!;
}