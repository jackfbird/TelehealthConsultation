using TelehealthConsultation.Models;

namespace TelehealthConsultation.Interfaces
{
    public interface ITelehealthService
    {
        Task<Patient> GetPatientInfoAsync(int patientId);
    }
}