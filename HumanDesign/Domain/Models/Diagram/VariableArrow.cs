using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Domain.Models.Diagram;

[Table("variable_arrow")]
public class VariableArrow
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("design_id")]
    public Guid DesignId { get; set; }
    public bool IsLeft { get; set; }
    public int Color { get; set; }
    public int Tone { get; set; }
    public int Base { get; set; }
}