using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models;

[Table("country")]
public class CountryVm
{
    [Key]
    [Column(TypeName = "INT(10)")]
    public int countryId { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(50)")]
    public string country { get; set; }

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

    public ICollection<CityVm> Cities { get; set;}
}
