using Microsoft.Extensions.DependencyInjection;
using WebCalendar.Application.Events.EventsCreating;
using WebCalendar.Application.Events.EventsDeleting;
using WebCalendar.Application.Events.EventsReciever;
using WebCalendar.Application.Events.EventsUpdating;

namespace WebCalendar.Application.Events
{
    public static class EventsBindings
    {
        public static IServiceCollection AddEventsBindings(this IServiceCollection services)
        {
            services.AddScoped<IEventCreator, EventCreator>();
            services.AddScoped<IEventUpdator, EventUpdater>();
            services.AddScoped<IEventDeletor, EventDeleter>();
            services.AddScoped<IEventReciever, EventReciever>();

            return services;
        }
    }
}
