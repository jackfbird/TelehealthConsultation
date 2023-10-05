using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TelehealthKafkaConsumer.Settings;

namespace TelehealthKafkaConsumer.Services
{
    public class KafkaConsumerService
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly KafkaConsumerSettings _settings;

        public KafkaConsumerService(ILogger<KafkaConsumerService> logger, IOptions<KafkaConsumerSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;

            var config = new ConsumerConfig
            {
                GroupId = _settings.GroupId,
                BootstrapServers = _settings.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public async void Listen(CancellationToken cancellationToken)
        {
            //create the topic if required
            using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = _settings.BootstrapServers }).Build())
            {
                try
                {
                    await adminClient.CreateTopicsAsync(new TopicSpecification[] {
                    new TopicSpecification { Name = _settings.Topic, ReplicationFactor = 1, NumPartitions = 1 } });
                }
                catch (CreateTopicsException e)
                {
                    Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
                }
            }

            _consumer.Subscribe(_settings.Topic);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(cancellationToken);
                        if (consumeResult.Message != null)
                        {
                            _logger.LogInformation($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");
                            Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");
                        }
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError($"Consume error: {e.Error.Reason}");
                        Console.WriteLine($"Consume error: {e.Error.Reason}");

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Exception error: {ex.Message}");
                        Console.WriteLine($"Exception error: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception error: {ex.Message}");
            }
            finally
            {
                _consumer.Close();
            }
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }
    }
}