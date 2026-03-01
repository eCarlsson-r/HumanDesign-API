using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities.Reference;

[Table("profile")]
public class ProfileEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code1")]
    public int Code1 { get; set; }

    [Column("code2")]
    public int Code2 { get; set; }

    [Column("name")]
    public string Name { get; set; } = default!;

    [Column("preview")]
    public string? Preview { get; set; }
    [Column("summary")]
    public string? Summary { get; set; }

    [Column("detail")]
    public string? Detail { get; set; }

    [Column("file_id")]
    public int? FileId { get; set; }
}
