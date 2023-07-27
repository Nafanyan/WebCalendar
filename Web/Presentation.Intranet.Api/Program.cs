using Infrastructure;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using Application;

namespace Presentation.Intranet.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
                .Build();

            string connectionString = configuration["ConnectionString"];
            builder.Services.AddDbContext<WebCalendarDbContext>(db => db.UseNpgsql(connectionString,
                 db => db.MigrationsAssembly("Infrastructure.Migration")));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationBindings();
            builder.Services.AddInfrastructureBindings();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins(configuration["WithOrigins:LocalHost"])
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}