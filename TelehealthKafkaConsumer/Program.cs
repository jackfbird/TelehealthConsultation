using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelehealthKafkaConsumer.Services;
using TelehealthKafkaConsumer.Settings;

namespace TelehealthKafkaConsumer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var host = CreateHostBuilder(args, configuration).Build();

            var kafkaConsumerService = host.Services.GetRequiredService<KafkaConsumerService>();
            kafkaConsumerService.Start();

            host.Run();
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