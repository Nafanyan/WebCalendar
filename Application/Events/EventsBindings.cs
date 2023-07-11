using Application.Events.Commands.CreateEvent;
using Application.Events.Commands.DeleteEvent;
using Application.Events.Commands.UpdateEvent;
using Application.Events.DTOs;
using Application.Events.Queries.EventQuery;
using Application.Interfaces;
using Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace WebCalendar.Application.Events
{
    public static class EventsBindings
    {
        public static IServiceCollection AddEventsBindings(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<EventCreateCommand>, EventCreateCommandHandler>();
            services.AddScoped<ICommandHandler<EventUpdateCommand>, EventUpdateCommandHandler>();
            services.AddScoped<ICommandHandler<EventDeleteCommand>, DeleteEventCommandHandler>();

            services.AddScoped<IQueryHandler<EventQueryDto, EventQuery>, EventQueryHandler>();

            services.AddScoped<IAsyncValidator<EventCreateCommand>, EventCreateCommandValidator>();
            services.AddScoped<IAsyncValidator<EventDeleteCommand>, DeleteEventCommandValidator>();
            services.AddScoped<IAsyncValidator<EventUpdateCommand>, EventUpdateCommandValidator>();

            services.AddScoped<IAsyncValidator<EventQuery>, EventQueryValidator>();
            return services;
        }
    }
}
