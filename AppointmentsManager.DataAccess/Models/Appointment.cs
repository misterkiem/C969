using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentsManager.DataAccess.Models
{
    public class Appointment
    {
        [Key]
        [Column(TypeName = "INT(10)")]
        public int appointmentId { get; set; }

        [Column(TypeName = "INT(10)")]

        public int customerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "INT")]
        public int userId { get; set; }
        public User User { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string title { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string description { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string location { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string contact { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public string type { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string url { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime start { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime end { get; set; }

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
    }
}