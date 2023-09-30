using System.ComponentModel.DataAnnotations;

namespace TelehealthConsultation.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }  

        [Required]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }  

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending"; 
    }
}