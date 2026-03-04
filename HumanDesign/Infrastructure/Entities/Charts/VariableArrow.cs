using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities.Charts;

[Table("variable_arrow")]
public class VariableArrow
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("design_id")]
    public Guid DesignId { get; set; }
    [Column("is_left")]
    public bool IsLeft { get; set; }
    [Column("color")]
    public int Color { get; set; }
    [Column("tone")]
    public int Tone { get; set; }
    [Column("base")]
    public int Base { get; set; }
    public Design Design { get; set; } = default!;
}