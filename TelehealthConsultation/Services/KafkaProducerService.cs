using Confluent.Kafka;
using System.Text.Json;
using TelehealthConsultation.Interfaces;
using TelehealthConsultation.Models;
using TelehealthConsultation.Settings;

namespace TelehealthConsultation.Services
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly ILogger<KafkaProducerService> _logger;
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerService(KafkaSettings kafkaSettings, ILogger<KafkaProducerService> logger)
        {
            var config = new ProducerConfig { BootstrapServers = kafkaSettings.BootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
            _logger = logger;   
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
                _logger.LogInformation($"Kafka Producer Succeeded: Booking start time: {booking.StartTime}, Message value: {message.Value}");

            }
            catch (ProduceException<Null, string> e)
            {
                _logger.LogError($"Kafka Producer failed: {e.Error.Reason}");
                throw new Exception($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}