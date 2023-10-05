using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System.Diagnostics;
using TelehealthKafkaConsumer.Protos;

namespace TelehealthKafkaConsumer.Services
{
    internal class TelehealthGrpcClientService
    {
        public TelehealthGrpcClientService()
        {
        }

        public static void CallGrpcService()
        {
            try
            {

                using var channel = GrpcChannel.ForAddress("http://localhost:443");
                var client = new Telehealth.TelehealthClient(channel);
                var reply = client.BookAppointment(
                                  new BookAppointmentRequest
                                  {
                                      DoctorId = 1,
                                      PatientId = 1,
                                      StartTime = Timestamp.FromDateTime(DateTime.Now.AddDays(2).ToUniversalTime()),
                                      EndTime = Timestamp.FromDateTime(DateTime.Now.AddDays(2.1).ToUniversalTime()),
                                  });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
