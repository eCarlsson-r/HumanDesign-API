using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("variables")]
public class VariableSet
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("design_id")]
    public Guid DesignId { get; set; }
    [Column("digestion")]
    public string Digestion { get; set; } = default!;
    [Column("cognition")]
    public string Cognition { get; set; } = default!;
    [Column("motivation")]
    public string Motivation { get; set; } = default!;
    [Column("perspective")]
    public string Perspective { get; set; } = default!;
    [Column("environment")]
    public string Environment { get; set; } = default!;

    public Design Design { get; set; } = default!;
}