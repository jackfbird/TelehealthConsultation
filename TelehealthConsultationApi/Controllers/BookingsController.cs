using Microsoft.AspNetCore.Mvc;
using TelehealthConsultationApi.Interfaces;
using TelehealthConsultationApi.Models;

namespace TelehealthConsultationApi.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ITelehealthGrpcClientService _grpcServiceClient;

        public BookingsController(ITelehealthGrpcClientService grpcServiceClient)
        {
            _grpcServiceClient = grpcServiceClient;
        }

        [HttpPost]
        public IActionResult CreateBooking([FromBody] Booking request)
        {
            try
            {
                // Call the gRPC service to create a booking
                var response = _grpcServiceClient.CreateBooking(request);

                // Handle the response as needed
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Handle errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}