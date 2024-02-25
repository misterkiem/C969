using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models;

[Table("address")]
public class Address : DbModel
{
    [Key]
    [Column(TypeName = "INT(10)")]
    public int addressId { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(50)")]
    public string address { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(50)")]
    public string address2 { get; set; }

    [Column(TypeName = "INT(10)")]
    public int cityId { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(10)")]
    public string postalCode { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(20)")]
    public string phone { get; set; }


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

    public City City { get; set; }

    public ICollection<Customer> Customers { get; set; }
}