using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("cross")]
public class IncarnationCrossEntity
{
    [Key]
    [Column("cross-code")]
    public string CrossCode { get; set; } = null!;

    [Column("cross-name")]
    public string? CrossName { get; set; }

    [Column("cross-type")]
    public string? CrossType { get; set; }

    [Column("cross1")]
    public int? Cross1 { get; set; }

    [Column("cross2")]
    public int? Cross2 { get; set; }

    [Column("cross3")]
    public int? Cross3 { get; set; }

    [Column("cross4")]
    public int? Cross4 { get; set; }
}
