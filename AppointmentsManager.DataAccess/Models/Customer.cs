using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models;

[Table("customer")]
public class Customer : DbModel
{
    [Key]
    [Column(TypeName = "INT(10)")]
    public int customerId { get; set; }

    [NotMapped]
    public override int Id => customerId;

    [NotMapped]
    public override Type Type => typeof(Customer);

    [Required]
    [Column(TypeName = "VARCHAR(45)")]
    public string customerName { get; set; }

    [Column(TypeName = "INT(10)")]
    public int addressId { get; set; }

    [Column(TypeName = "TINYINT")]
    public byte active { get; set; }

    public Address Address { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}