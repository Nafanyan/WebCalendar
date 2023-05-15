using Application.Events.Commands;
using Application.Events.Commands.CreateEvent;
using Application.Events.Commands.DeleteEvent;
using Application.Events.Commands.UpdateEvent;
using Application.Events.Queries;
using Application.Events.Queries.GetEvent;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace WebCalendar.Application.Events
{
    public static class EventsBindings
    {
        public static IServiceCollection AddEventsBindings(this IServiceCollection services)
        {
            services.AddScoped<IEventCommandHandler<AddEventCommand>, AddEventCommandHandler>();
            services.AddScoped<IEventCommandHandler<UpdateEventCommand>, UpdateEventCommandHandler>();
            services.AddScoped<IEventCommandHandler<DeleteEventCommand>, DeleteEventCommandHandler>();

            services.AddScoped<IEventQueryHandler<Event, GetEventQuery>, GetEventQueryHandler>();
            return services;
        }
    }
}
