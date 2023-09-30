using TelehealthConsultation.Data;
using TelehealthConsultation.Interfaces;
using TelehealthConsultation.Models;
using TelehealthConsultation.Protos;

namespace TelehealthConsultation.Services
{
    public class ConsultationService : IConsultationService
    {
        private readonly AppDbContext _context;
        private readonly IKafkaProducerService _kafkaProducerService;

        public ConsultationService(AppDbContext context, IKafkaProducerService kafkaProducerService)
        {
            _context = context;
            _kafkaProducerService = kafkaProducerService;
        }

        public async Task<BookAppointmentResponse> BookAppointmentAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            await _kafkaProducerService.ProduceAsync("bookingsTopic", booking);

            return new BookAppointmentResponse
            {
                Success = true,
                Message = "Booking was successful."
            };
        }
    }
}