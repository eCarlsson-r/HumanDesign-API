using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("attributes")]
public class AttributeEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("property")]
    public string Property { get; set; } = default!;
    [Column("value")]
    public string Value { get; set; } = default!;
    [Column("title")]
    public string Title { get; set; } = default!;

    [Column("preview")]
    public string? Preview { get; set; }

    [Column("summary")]
    public string? Summary { get; set; }

    [Column("detail")]
    public string? Detail { get; set; }
}