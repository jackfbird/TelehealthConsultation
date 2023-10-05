using System.ComponentModel.DataAnnotations;

namespace TelehealthConsultationApi.Models
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
        public ICollection<TimeSlot> TimeSlots { get; set; }
    }
}
