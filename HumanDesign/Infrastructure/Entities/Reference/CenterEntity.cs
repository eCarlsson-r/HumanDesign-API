using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities.Reference;

[Table("center")]
public class CenterEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string CenterName { get; set; } = "";
    [Column("definition")]
    public string Definition { get; set; } = ""; // defined / open
    [Column("preview")]
    public string? Preview { get; set; } = default!;
    [Column("summary")]
    public string? Summary { get; set; } = default!;
    [Column("detail")]
    public string? Detail { get; set; } = default!;
    [Column("file_id")]
    public int? FileId { get; set; }
}