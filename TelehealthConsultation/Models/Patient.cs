using System.ComponentModel.DataAnnotations;

namespace TelehealthConsultation.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}