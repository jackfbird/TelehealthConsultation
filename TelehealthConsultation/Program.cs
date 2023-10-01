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

            // Registering services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddGrpc();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connection));

            // Kafka Configurations
            var kafkaSettings = new KafkaSettings
            {
                BootstrapServers = "localhost:29092",
                TopicName = "telehealth_bookings"
            };

            builder.Services.AddSingleton(kafkaSettings);
            builder.Services.AddScoped<IKafkaProducerService, KafkaProducerService>();

            builder.Services.AddScoped<ITelehealthService, TelehealthService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}