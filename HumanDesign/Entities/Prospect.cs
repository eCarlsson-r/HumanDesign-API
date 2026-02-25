using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("prospect")]
public class Prospect
{
    [Key]
    [Column("prospect-code")]
    public int ProspectCode { get; set; }

    [Column("prospect-name")]
    [StringLength(255)]
    public string ProspectName { get; set; } = string.Empty;

    [Column("prospect-unique")]
    [StringLength(255)]
    public string? ProspectUnique { get; set; }

    [Column("prospect-pob")]
    [StringLength(255)]
    public string? ProspectPob { get; set; }

    [Column("prospect-timezone")]
    [StringLength(255)]
    public string? ProspectTimezone { get; set; }

    [Column("prospect-dob")]
    public string? ProspectDob { get; set; }

    [Column("prospect-tob")]
    public string? ProspectTob { get; set; }

    [Column("prospect-mobile")]
    [StringLength(50)]
    public string? ProspectMobile { get; set; }

    [Column("prospect-email")]
    [StringLength(255)]
    public string? ProspectEmail { get; set; }

    [Column("prospect-refer")]
    [StringLength(255)]
    public string? ProspectRefer { get; set; }
}
