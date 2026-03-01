using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities.Reference;

[Table("gate")]
public class GateEntity
{
    [Key]
    [Column("id")]
    public int Number { get; set; }

    [Column("title")]
    public string Title { get; set; } = default!;

    [Column("preview")]
    public string? Preview { get; set; } = default!;

    [Column("summary")]
    public string? Summary { get; set; } = default!;

    [Column("detail")]
    public string? Detail { get; set; } = default!;

    [Column("file_id")]
    public int? FileId { get; set; }
}
