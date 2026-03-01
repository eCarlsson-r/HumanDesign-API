using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities.Reference;

[Table("channel")]
public class ChannelEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = default!;
    [Column("gate_a")]
    public int GateA { get; set; }
    [Column("gate_b")]
    public int GateB { get; set; }
    [Column("theme")]
    public string Theme { get; set; } = string.Empty;
    [Column("preview")]
    public string? Preview { get; set; } = default!;
    [Column("summary")]
    public string? Summary { get; set; } = default!;
    [Column("detail")]
    public string? Detail { get; set; } = default!;
    [Column("gift")]
    public string? Gift { get; set; }
    [Column("shadow")]
    public string? Shadow { get; set; }
    [Column("file_id")]
    public int? FileId { get; set; }

}
