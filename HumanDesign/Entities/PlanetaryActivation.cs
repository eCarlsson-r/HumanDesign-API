using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("planetary_activations")]
public class PlanetaryActivation
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("design_id")]
    public Guid DesignId { get; set; }
    [Column("planet")]
    public string Planet { get; set; } = default!;
    [Column("is_design")]
    public bool IsDesign { get; set; }
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

    public Design Design { get; set; } = default!;
}