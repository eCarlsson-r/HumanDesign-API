using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("profile")]
public class ProfileEntity
{
    [Key]
    [Column("profile-id")]
    public int ProfileId { get; set; }

    [Column("profile-code1")]
    public string? ProfileCode1 { get; set; }

    [Column("profile-code2")]
    public string? ProfileCode2 { get; set; }

    [Column("profile-name")]
    public string? ProfileName { get; set; }

    [Column("profile-summary")]
    public string? ProfileSummary { get; set; }

    [Column("profile-detail")]
    public string? ProfileDetail { get; set; }

    [Column("profile-img-no")]
    public int? ProfileImgNo { get; set; }
}
