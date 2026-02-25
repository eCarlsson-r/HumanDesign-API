using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("files")]
public class FileRecord
{
    [Key]
    [Column("file-id")]
    public int FileId { get; set; }

    [Column("file-name")]
    [StringLength(255)]
    public string? FileName { get; set; }

    [Column("file-type")]
    [StringLength(100)]
    public string? FileType { get; set; }

    [Column("file-ext")]
    [StringLength(10)]
    public string? FileExt { get; set; }

    [Column("file-size")]
    public double? FileSize { get; set; }

    [Column("file-upload-date")]
    public string? FileUploadDate { get; set; }
}
