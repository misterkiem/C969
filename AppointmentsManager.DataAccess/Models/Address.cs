using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models;

[Table("address")]
public class Address : DbModel
{ 
    [Key]
    [Column(TypeName = "INT(10)")]
    public int addressId { get; set; }

    [NotMapped]
    public override int Id => addressId;

    [Required]
    [Column(TypeName = "VARCHAR(50)")]
    public string address { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "VARCHAR(50)")]
    public string address2 { get; set; }

    [Column(TypeName = "INT(10)")]
    public int cityId { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(10)")]
    public string postalCode { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "VARCHAR(20)")]
    public string phone { get; set; } = string.Empty;

    public City City { get; set; }

    public ICollection<Customer> Customers { get; set; }
}