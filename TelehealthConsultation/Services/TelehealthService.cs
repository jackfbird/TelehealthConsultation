using TelehealthConsultation.Data;
using TelehealthConsultation.Interfaces;
using TelehealthConsultation.Models;

namespace TelehealthConsultation.Services
{
    public class TelehealthService : ITelehealthService
    {
        private readonly AppDbContext _context; 

        public TelehealthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> GetPatientInfoAsync(int patientId)
        {
            return await _context.Patients.FindAsync(patientId) ?? throw new InvalidOperationException("Patient not found");
        }
    }
}