using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelehealthConsultationApi.Data;

namespace TelehealthConsultationApi.Controllers
{
    [Route("api/timeslots")]
    [ApiController]
    public class TimeSlotsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TimeSlotsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/timeslots
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTimeSlots()
        {
            try
            {
                // Get current date and time
                var currentDateTime = DateTime.Now;

                // Query for future available time slots
                var availableTimeSlots = await _context.TimeSlots
                    .Where(ts => ts.BookingDate >= currentDateTime.Date && ts.StartTime >= currentDateTime)
                    .ToListAsync();

                return Ok(availableTimeSlots);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}