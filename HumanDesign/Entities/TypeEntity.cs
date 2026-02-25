using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("type")]
public class TypeEntity
{
    [Key]
    [Column("type-code")]
    public int TypeCode { get; set; }

    [Column("type-name")]
    public string? TypeName { get; set; }

    [Column("type-strategy")]
    public int? TypeStrategy { get; set; }

    [Column("type-characteristic")]
    public int? TypeCharacteristic { get; set; }

    [Column("type-theme")]
    public int? TypeTheme { get; set; }

    [Column("type-preview")]
    public string? TypePreview { get; set; }

    [Column("type-summary")]
    public string? TypeSummary { get; set; }

    [Column("type-detail")]
    public string? TypeDetail { get; set; }

    [Column("type-img-no")]
    public int? TypeImgNo { get; set; }
}
