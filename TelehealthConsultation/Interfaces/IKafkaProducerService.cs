using TelehealthConsultation.Models;

namespace TelehealthConsultation.Interfaces
{
    public interface IKafkaProducerService
    {
        Task ProduceAsync(string topic, Booking booking);
    }
}
