﻿using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandValidation :  IAsyncValidator<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandValidation(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ValidationResult> ValidationAsync(DeleteEventCommand command)
        {
            if (command.StartEvent == null)
            {
                return ValidationResult.Fail("The start date cannot be empty/cannot be null";);
            }

            if (command.EndEvent == null)
            {
                return ValidationResult.Fail("The end date cannot be empty/cannot be null");
            }

            if (command.StartEvent > command.EndEvent)
            {
                return ValidationResult.Fail("The start date cannot be later than the end date");
            }

            if (command.StartEvent.ToShortDateString != command.EndEvent.ToShortDateString)
            {
                return ValidationResult.Fail("The event must occur within one day");
            }

            EventPeriod eventPeriod = new EventPeriod(command.StartEvent, command.EndEvent);
            if (!await _eventRepository.ContainsAsync(command.UserId, eventPeriod))
            {
                return ValidationResult.Fail("An event with such a time does not exist");
            }

            return ValidationResult.Ok();
        }
    }
}
