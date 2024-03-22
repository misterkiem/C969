using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models;

[Table("appointment")]
public class Appointment : DbModel
{
    [Key]
    [Column(TypeName = "INT(10)")]
    public int appointmentId { get; set; }

    [NotMapped]
    public override int Id => appointmentId;

    [NotMapped]
    public override Type Type => typeof(Appointment);

    [Column(TypeName = "INT(10)")]
    public int customerId { get; set; }

    public Customer Customer { get; set; }

    [Column(TypeName = "INT")]
    public int userId { get; set; }

    public User User { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(255)")]
    public string title { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "TEXT")]
    public string description { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "TEXT")]
    public string location { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "TEXT")]
    public string contact { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "TEXT")]
    public string type { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(255)")]
    public string url { get; set; } = string.Empty;

    [Column(TypeName = "DATETIME")]
    public DateTime start { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime end { get; set; }
}