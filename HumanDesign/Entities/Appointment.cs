using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanDesignApi.Entities;

[Table("appointment")]
public class Appointment
{
    [Key]
    [Column("appointment-code")]
    public int AppointmentCode { get; set; }

    [Column("appointment-prospect")]
    public int AppointmentProspect { get; set; }

    [Column("appointment-date")]
    public string? AppointmentDate { get; set; }

    [Column("appointment-time")]
    public string? AppointmentTime { get; set; }
}
