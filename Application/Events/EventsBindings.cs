using Application.Events.Commands.CreateEvent;
using Application.Events.Commands.DeleteEvent;
using Application.Events.Commands.UpdateEvent;
using Application.Events.Queries.GetEvent;
using Microsoft.Extensions.DependencyInjection;

namespace WebCalendar.Application.Events
{
    public static class EventsBindings
    {
        public static IServiceCollection AddEventsBindings(this IServiceCollection services)
        {
            services.AddScoped<IAddEventCommandHandler, AddEventCommandHandler>();
            services.AddScoped<IUpdateEventCommandHandler, UpdateEventCommandHandler>();
            services.AddScoped<IDeleteEventCommandHandler, DeleteEventCommandHandler>();

            services.AddScoped<IGetEventQueryHandler, GetEventQueryHandler>();
            return services;
        }
    }
}
