using Microsoft.EntityFrameworkCore;
using TelehealthConsultation.Data;
using TelehealthConsultation.Interfaces;
using TelehealthConsultation.Services;
using TelehealthConsultation.Settings;

namespace TelehealthConsultation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration
            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
            }

            var connection = builder.Environment.IsDevelopment()
                ? builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")
                : Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");

            builder.Services.AddGrpc();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connection));

            // Kafka Configurations
            var kafkaSettings = new KafkaSettings
            {
                BootstrapServers = builder.Configuration["Kafka:BootstrapServers"],
                Topic = builder.Configuration["Kafka:Topic"]
            };

            builder.Services.AddSingleton(kafkaSettings);
            builder.Services.AddScoped<IKafkaProducerService, KafkaProducerService>();
            builder.Services.AddScoped<IConsultationService, ConsultationService>();
            builder.Services.AddScoped<ITelehealthService, TelehealthService>();

            var app = builder.Build();

            // Configure the gRPC service.
            app.MapGrpcService<TelehealthGrpcService>();
            app.Run();
        }
    }
}