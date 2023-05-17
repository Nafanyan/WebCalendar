﻿using Application.Events.Commands.CreateEvent;
using Application.Events.Commands.DeleteEvent;
using Application.Events.Commands.UpdateEvent;
using Application.Events.Queries.GetEvent;
using Application.Interfaces;
using Application.Validation;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace WebCalendar.Application.Events
{
    public static class EventsBindings
    {
        public static IServiceCollection AddEventsBindings(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateEventCommand>, CreateEventCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateEventCommand>, UpdateEventCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteEventCommand>, DeleteEventCommandHandler>();

            services.AddScoped<IQueryHandler<Event, GetEventQuery>, GetEventQueryHandler>();

            services.AddScoped<IAsyncValidator<CreateEventCommand>, CreateEventCommandValidation>();
            services.AddScoped<IAsyncValidator<DeleteEventCommand>, DeleteEventCommandValidation>();
            services.AddScoped<IAsyncValidator<UpdateEventCommand>, UpdateEventCommandValidation>();

            services.AddScoped<IAsyncValidator<GetEventQuery>, GetEventQueryValidation>();
            return services;
        }
    }
}