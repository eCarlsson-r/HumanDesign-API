using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities.Reference;

[Table("cross")]
public class CrossEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = default!;

    [Column("type")]
    public string Type { get; set; } = default!;

    [Column("cross1")]
    public int? Cross1 { get; set; }

    [Column("cross2")]
    public int? Cross2 { get; set; }

    [Column("cross3")]
    public int? Cross3 { get; set; }

    [Column("cross4")]
    public int? Cross4 { get; set; }
}
