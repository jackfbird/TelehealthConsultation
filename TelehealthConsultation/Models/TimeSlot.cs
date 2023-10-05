namespace TelehealthConsultation.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }

        // Foreign key for Doctor
        public int DoctorId { get; set; }

        // Navigation property to Doctor
        public Doctor? Doctor { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}