using TelehealthConsultation.Models;
using TelehealthConsultation.Protos;

namespace TelehealthConsultation.Interfaces
{
    public interface IConsultationService
    {
        Task<BookAppointmentResponse> BookAppointmentAsync(Booking booking);
    }
}