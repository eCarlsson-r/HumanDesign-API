using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HumanDesign.Infrastructure.Entities;

[Table("users")]
public class UserEntity : IdentityUser<Guid>
{
    [Required]
    [Column("name")]
    public string FullName { get; set; } = "";
    [Column("referral_code")]
    public string ReferralCode { get; set; } = "";
    [Column("role")]
    public string Role { get; set; } = "";

    [ForeignKey("parent_id")]
    public Guid? ParentId { get; set; }
    public UserEntity? Parent { get; set; }

    public ICollection<UserEntity> Children { get; set; } = new List<UserEntity>();
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}