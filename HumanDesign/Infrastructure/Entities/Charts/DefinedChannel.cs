using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities.Charts;

[Table("defined_channels")]
public class DefinedChannel
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("design_id")]
    public Guid DesignId { get; set; }
    [Column("channel_id")]
    public string ChannelId { get; set; } = default!;

    [Column("gate_a")]
    public int GateA { get; set; }
    [Column("gate_b")]
    public int GateB { get; set; }

    public Design Design { get; set; } = default!;
}