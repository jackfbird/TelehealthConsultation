using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System.Diagnostics;
using TelehealthConsultationApi.Interfaces;
using TelehealthConsultationApi.Models;
using TelehealthConsultationApi.Protos;

namespace TelehealthConsultationApi.Services
{
    public class TelehealthGrpcClientService : ITelehealthGrpcClientService
    {
        public TelehealthGrpcClientService()
        {
        }

        public bool CreateBooking(Booking booking)
        {
            try
            {
                using var channel = GrpcChannel.ForAddress("http://localhost:443");
                var client = new Telehealth.TelehealthClient(channel);
                var reply = client.BookAppointment(
                                  new BookAppointmentRequest
                                  {
                                      DoctorId = booking.DoctorId == 0 ? 1 : booking.DoctorId,
                                      PatientId = booking.PatientId == 0 ? 1 : booking.PatientId,
                                      StartTime = Timestamp.FromDateTime(booking.StartTime.ToUniversalTime()),
                                      EndTime = Timestamp.FromDateTime(booking.StartTime.AddMinutes(15).ToUniversalTime()),
                                  });

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }
    }
}