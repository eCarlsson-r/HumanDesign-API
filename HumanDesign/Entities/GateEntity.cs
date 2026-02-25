using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("gate")]
public class GateEntity
{
    [Key]
    [Column("gate-code")]
    public int GateCode { get; set; }

    [Column("gate-title")]
    public string? GateTitle { get; set; }

    [Column("gate-preview")]
    public string? GatePreview { get; set; }

    [Column("gate-summary")]
    public string? GateSummary { get; set; }

    [Column("gate-detail")]
    public string? GateDetail { get; set; }

    [Column("gate-img-no")]
    public int? GateImgNo { get; set; }
}
