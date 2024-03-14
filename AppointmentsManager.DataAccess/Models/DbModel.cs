using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models;
public abstract class DbModel
{
    [NotMapped]
    public abstract int Id { get; }


    [Column(TypeName = "DATETIME")]
    public DateTime createDate { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(40)")]
    public string createdBy { get; set; }

    [Column(TypeName = "TIMESTAMP")]
    public DateTime lastUpdate { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(40)")]
    public string lastUpdateBy { get; set; }

    public DbModel()
    {
        createDate = DateTime.Now;
        lastUpdate = DateTime.Now;
        createdBy = "Appointments manager";
        lastUpdateBy = "Appointments manager";
    }
}