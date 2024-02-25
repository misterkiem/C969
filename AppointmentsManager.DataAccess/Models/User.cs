
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models
{
    public class User
    {
        [Key]
        [Column(TypeName = "INT")]
        public int userId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string userName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string password { get; set; }

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

        public ICollection<Appointment> Appointments { get; set; }
    }
}
