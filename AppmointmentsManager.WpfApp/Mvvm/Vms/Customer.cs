using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models;

[Table("customer")]
public class Customer
{
    [Key]
    [Column(TypeName = "INT(10)")]
    public int customerId { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(45)")]
    public string customerName { get; set; }

    [Column(TypeName = "INT(10)")]
    public int addressId { get; set; }

    [Column(TypeName = "TINYINT")]
    public byte active { get; set; }

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

    public Address Address { get; set; }

    public ICollection<AppointmentVm> Appointments { get; set; }
}