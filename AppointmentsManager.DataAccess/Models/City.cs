using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models;

[Table("city")]
public class City : DbModel
{
    [Key]
    [Column(TypeName = "INT(10)")]
    public int cityId { get; set; }

    [NotMapped]
    public override int Id => cityId;

    [Required]
    [Column(TypeName = "VARCHAR(50)")]
    public string city { get; set; }

    [Column(TypeName = "INT(10)")]
    public int countryId { get; set; }

    public Country Country { get; set; }

    public ICollection<Address> Addresses { get; set; }
}