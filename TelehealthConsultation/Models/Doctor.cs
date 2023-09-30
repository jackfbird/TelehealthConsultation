using System.ComponentModel.DataAnnotations;

namespace TelehealthConsultation.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Specialization { get; set; }
    }
}