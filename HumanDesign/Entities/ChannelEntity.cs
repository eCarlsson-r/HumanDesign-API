using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("channel")]
public class ChannelEntity
{
    [Key]
    [Column("channel-code")]
    public string ChannelCode { get; set; } = null!;

    [Column("channel-name")]
    public string? ChannelName { get; set; }

    [Column("channel-preview")]
    public string? ChannelPreview { get; set; }

    [Column("channel-summary")]
    public string? ChannelSummary { get; set; }

    [Column("channel-detail")]
    public string? ChannelDetail { get; set; }

    [Column("channel-img-no")]
    public int? ChannelImgNo { get; set; }
}
