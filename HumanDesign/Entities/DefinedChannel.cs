using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("defined_channels")]
public class DefinedChannel
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("design_id")]
    public Guid DesignId { get; set; }
    [Column("channel_id")]
    public int ChannelId { get; set; }

    public Design Design { get; set; } = default!;
}