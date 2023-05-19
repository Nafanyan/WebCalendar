using Infrastructure;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using WebCalendar.Application;

namespace Presentation.Intranet.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("WebCalendar");
            builder.Services.AddDbContext<WebCalendarDbContext>(db => db.UseNpgsql(connectionString,
                db => db.MigrationsAssembly("Infrastructure.Migration")));

            // Add services to the container.

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}