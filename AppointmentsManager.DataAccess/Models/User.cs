using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models;

[Table("user")]
public class User : DbModel
{
    [Key]
    [Column(TypeName = "INT")]
    public int userId { get; set; }

    [NotMapped]
    public override int Id => userId;

    [Required]
    [Column(TypeName = "VARCHAR(50)")]
    public string userName { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(50)")]
    public string password { get; set; }

    [Column(TypeName = "TINYINT")]
    public byte active { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}