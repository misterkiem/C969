using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models;

[Table("country")]
public class Country : DbModel
{
    [Key]
    [Column(TypeName = "INT(10)")]
    public int countryId { get; set; }

    [NotMapped]
    public override int Id => countryId;

    [Required]
    [Column(TypeName = "VARCHAR(50)")]
    public string country { get; set; }

    public ICollection<City> Cities { get; set; }
}