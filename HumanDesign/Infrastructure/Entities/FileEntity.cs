using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesign.Infrastructure.Entities;

[Table("file")]
public class FileEntity
{
    [Key]
    [Column("id")]
    public int FileNo { get; set; }

    [Column("name")]
    public string? FileName { get; set; }

    [Column("extension")]
    public string? Extension { get; set; }

    [Column("path")]
    public string? Path { get; set; }

    [Column("size")]
    public long? Size { get; set; }
}