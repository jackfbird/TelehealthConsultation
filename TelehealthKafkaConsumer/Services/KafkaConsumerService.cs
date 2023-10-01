using Confluent.Kafka;
using Microsoft.Extensions.Options;
using TelehealthKafkaConsumer.Settings;

namespace TelehealthKafkaConsumer.Services
{
    public class KafkaConsumerService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly KafkaConsumerSettings _settings;

        public KafkaConsumerService(IOptions<KafkaConsumerSettings> settings)
        {
            _settings = settings.Value;

            var config = new ConsumerConfig
            {
                GroupId = _settings.GroupId,
                BootstrapServers = _settings.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        }

        public void Start()
        {
            Listen(message =>
            {
                Console.WriteLine($"Received: {message}");
            });
        }

        public void Listen(Action<string> messageHandler)
        {
            _consumer.Subscribe(_settings.Topic); 
        }
    }
}