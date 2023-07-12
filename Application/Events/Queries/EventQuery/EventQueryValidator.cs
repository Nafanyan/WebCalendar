﻿using Application.Validation;
using Domain.Repositories;

namespace Application.Events.Queries.EventQuery
{
    public class EventQueryValidator : IAsyncValidator<EventQuery>
    {
        private readonly IEventRepository _eventRepository;

        public EventQueryValidator(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ValidationResult> ValidationAsync(EventQuery query)
        {
            if (query.StartEvent == null)
            {
                return ValidationResult.Fail("The start date cannot be empty/cannot be null");
            }

            if (query.EndEvent == null)
            {
                return ValidationResult.Fail("The end date cannot be empty/cannot be null");
            }

            if (query.StartEvent > query.EndEvent)
            {
                return ValidationResult.Fail("The start date cannot be later than the end date");
            }

            if (query.StartEvent.ToShortDateString() != query.EndEvent.ToShortDateString())
            {
                return ValidationResult.Fail("The event must occur within one day");
            }

            if (!await _eventRepository.ContainsAsync(query.UserId, query.StartEvent, query.EndEvent))
            {
                return ValidationResult.Fail("There is no event with this time for the current user");
            }

            return ValidationResult.Ok();
        }
    }
}