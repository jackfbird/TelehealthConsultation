using TelehealthConsultation.Data;
using TelehealthConsultation.Interfaces;
using TelehealthConsultation.Models;
using TelehealthConsultation.Protos;
using TelehealthConsultation.Settings;

namespace TelehealthConsultation.Services
{
    public class ConsultationService : IConsultationService
    {
        private readonly AppDbContext _context;
        private readonly KafkaSettings _kafkaSettings;
        private readonly IKafkaProducerService _kafkaProducerService;

        public ConsultationService(AppDbContext context, KafkaSettings kafkaSettings, IKafkaProducerService kafkaProducerService)
        {
            _context = context;
            _kafkaSettings = kafkaSettings;
            _kafkaProducerService = kafkaProducerService;
        }

        public async Task<BookAppointmentResponse> BookAppointmentAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            await _kafkaProducerService.ProduceAsync(_kafkaSettings.Topic, booking);

            return new BookAppointmentResponse
            {
                Success = true,
                Message = "Booking was successful."
            };
        }
    }
}