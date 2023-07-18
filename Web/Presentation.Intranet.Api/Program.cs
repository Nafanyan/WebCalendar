using Infrastructure;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using Application;
using Presentation.Intranet.Api.ConfigurationUtils;

namespace Presentation.Intranet.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // for release
            //var connectionString = builder.Configuration.GetConnectionString("WebCalendar");
            //for debug
            var connectionString = builder.Configuration.GetConnectionString("WebCalendarLocal");

           builder.Services.AddDbContext<WebCalendarDbContext>(db => db.UseNpgsql(connectionString,
                db => db.MigrationsAssembly("Infrastructure.Migration")));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddConfigurationUtilsBindings();
            builder.Services.AddApplicationBindings();
            builder.Services.AddInfrastructureBindings();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(builder => {
                builder.WithOrigins("http://localhost:3000")
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