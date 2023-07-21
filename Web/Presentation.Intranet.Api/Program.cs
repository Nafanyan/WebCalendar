using Infrastructure;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using Application;
using Infrastructure.ConfigurationUtils.DataBase;
using Infrastructure.ConfigurationUtils.CORS;

namespace Presentation.Intranet.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = String.Empty;
            if (builder.Environment.IsDevelopment())
            {
                connectionString = new DataBaseConfigurationDevelopment().GetConnectionString();
            }

            if (builder.Environment.IsProduction())
            {
                connectionString = new DataBaseConfigurationProduction().GetConnectionString();
            }

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

                app.UseCors(builder => {
                    builder.WithOrigins(new CorsConfigurationDevelopment().GetWithOrigins())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            }

            if (app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseCors(builder => {
                    builder.WithOrigins(new CorsConfigurationProduction().GetWithOrigins())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}