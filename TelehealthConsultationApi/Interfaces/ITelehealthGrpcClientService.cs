using TelehealthConsultationApi.Models;

namespace TelehealthConsultationApi.Interfaces
{
    public interface ITelehealthGrpcClientService
    {
        bool CreateBooking(Booking booking);
    }
}
