namespace TelehealthConsultationApi.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public Doctor? Doctor { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
