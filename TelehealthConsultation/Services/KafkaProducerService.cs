using Confluent.Kafka;
using System.Text.Json;
using TelehealthConsultation.Interfaces;
using TelehealthConsultation.Models;
using TelehealthConsultation.Settings;

namespace TelehealthConsultation.Services
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerService(KafkaSettings kafkaSettings)
        {
            var config = new ProducerConfig { BootstrapServers = kafkaSettings.BootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task ProduceAsync(string topic, Booking booking)
        {
            try
            {
                var message = new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(booking)
                };

                await _producer.ProduceAsync(topic, message);
            }
            catch (ProduceException<Null, string> e)
            {
                // Handle the exception based on your requirements
                throw new Exception($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}