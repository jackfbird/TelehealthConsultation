using Microsoft.EntityFrameworkCore;
using TelehealthConsultationApi.Data;
using TelehealthConsultationApi.Interfaces;
using TelehealthConsultationApi.Services;

namespace TelehealthConsultationApi
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

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connection));

            builder.Services.AddScoped<ITelehealthGrpcClientService, TelehealthGrpcClientService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:5173")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();
            // Configure CORS policy
            app.UseCors("VueCorsPolicy");
            app.Run();
        }
    }
}