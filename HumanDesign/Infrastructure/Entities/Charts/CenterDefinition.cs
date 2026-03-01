using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities.Charts;
[Table("center_definition")]
public class CenterDefinition
{
    [Key]
    public int Id { get; set; }

    public Guid DesignId { get; set; }

    public string CenterName { get; set; } = "";

    public string Definition { get; set; } = ""; // defined / open
}