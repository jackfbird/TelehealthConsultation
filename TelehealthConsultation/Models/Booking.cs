using System.ComponentModel.DataAnnotations;

namespace TelehealthConsultation.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }

        [Required]
        public int PatientID { get; set; }
        public Patient? Patient { get; set; }  // Navigation property

        [Required]
        public int DoctorID { get; set; }
        public Doctor? Doctor { get; set; }  // Navigation property

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";  // Default value
    }
}