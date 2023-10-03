using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelehealthKafkaConsumer.Services;
using TelehealthKafkaConsumer.Settings;

namespace TelehealthKafkaConsumer
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var builder = CreateHostBuilder(args, configuration).Build();

            TelehealthGrpcClientService.CallGrpcService();

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            var kafkaConsumerService = builder.Services.GetService<KafkaConsumerService>() ?? throw new NullReferenceException("kafkaConsumerService is null");
            kafkaConsumerService.Listen(cts.Token);
            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
     Host.CreateDefaultBuilder(args)
         .ConfigureServices((hostContext, services) =>
         {
             services.Configure<KafkaConsumerSettings>(configuration.GetSection("KafkaConsumerSettings"));
             services.AddSingleton<KafkaConsumerService>();
         });
    }
}