using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanDesign.Domain.Models.Diagram;

namespace HumanDesign.Infrastructure.Entities.Charts;

[Table("variables")]
public class VariableSet
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("design_id")]
    public Guid DesignId { get; set; }
    [Column("digestion")]
    public string Digestion { get; set; } = default!;
    [Column("reasoning")]
    public string Reasoning { get; set; } = default!;
    [Column("cognition")]
    public string Cognition { get; set; } = default!;
    [Column("motivation")]
    public string Motivation { get; set; } = default!;
    [Column("perspective")]
    public string Perspective { get; set; } = default!;
    [Column("environment")]
    public string Environment { get; set; } = default!;
    
    [Column("digestion_arrow")]
    public VariableArrow DigestionArrow { get; set; } = default!;
    [Column("environment_arrow")]
    public VariableArrow EnvironmentArrow { get; set; } = default!;
    [Column("awareness_arrow")]
    public VariableArrow AwarenessArrow { get; set; } = default!;
    [Column("perspective_arrow")]
    public VariableArrow PerspectiveArrow { get; set; } = default!;

    public Design Design { get; set; } = default!;
}