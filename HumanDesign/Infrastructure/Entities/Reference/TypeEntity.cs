using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities.Reference;

[Table("type")]
public class TypeEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = default!;

    [Column("strategy")]
    public int? Strategy { get; set; }

    [Column("characteristic")]
    public int? Characteristic { get; set; }

    [Column("theme")]
    public int? Theme { get; set; }

    [Column("preview")]
    public string? Preview { get; set; }

    [Column("summary")]
    public string? Summary { get; set; }

    [Column("detail")]
    public string? Detail { get; set; }

    [Column("file_id")]
    public int? FileId { get; set; }
}
