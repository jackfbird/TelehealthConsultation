using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TelehealthConsultation.Interfaces;
using TelehealthConsultation.Models;
using TelehealthConsultation.Protos;

namespace TelehealthConsultation.Services
{
    public class TelehealthGrpcService : Telehealth.TelehealthBase
    {
        private readonly IConsultationService _consultationService;
        private readonly ITelehealthService _telehealthService;

        public TelehealthGrpcService(
            IConsultationService consultationService,
            ITelehealthService telehealthService)
        {
            _consultationService = consultationService;
            _telehealthService = telehealthService;
        }

        public override async Task<BookAppointmentResponse> BookAppointment(BookAppointmentRequest request, ServerCallContext context)
        {
            var booking = new Booking
            {
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
                StartTime = request.StartTime.ToDateTime(),
                EndTime = request.EndTime.ToDateTime()
            };

            var result = await _consultationService.BookAppointmentAsync(booking);

            return new BookAppointmentResponse
            {
                Success = result.Success,
                Message = result.Message
            };
        }

        public override async Task<GetPatientInfoResponse> GetPatientInfo(GetPatientInfoRequest request, ServerCallContext context)
        {
            var patient = await _telehealthService.GetPatientInfoAsync(request.PatientId);

            return new GetPatientInfoResponse
            {
                PatientName = patient.Name,
                DateOfBirth = Timestamp.FromDateTime(patient.DateOfBirth.ToUniversalTime()),
                Email = patient.Email
            };
        }
    }
}