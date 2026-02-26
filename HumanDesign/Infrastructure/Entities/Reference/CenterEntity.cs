using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities.Reference;

[Table("center")]
public class CenterEntity
{
    [Key]
    public Guid Id { get; set; }

    public Guid DesignId { get; set; }

    public string CenterName { get; set; } = "";

    public string? Definition { get; set; } // defined / open
}