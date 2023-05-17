using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidation : IAsyncValidator<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandValidation(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ValidationResult> Validation(CreateEventCommand command)
        {
            if (command.Name == null)
            {
                return ValidationResult.Fail("The name of event cannot be empty/cannot be null");
            }

            if (command.StartEvent == null)
            {
                return ValidationResult.Fail("The start date cannot be empty/cannot be null");
            }

            if (command.StartEvent == null)
            {
                return ValidationResult.Fail("The end date cannot be empty/cannot be null");
            }

            if (command.StartEvent.ToShortDateString != command.EndEvent.ToShortDateString)
            {
                return ValidationResult.Fail("The event must occur within one day");
            }

            if (command.StartEvent > command.EndEvent)
            {
                return ValidationResult.Fail("The start date cannot be later than the end date");
            }

            EventPeriod eventPeriod = new EventPeriod(command.StartEvent, command.EndEvent);
            if (!await _eventRepository.Contains(command.UserId, eventPeriod))
            {
                return ValidationResult.Fail("This event is superimposed on the existing event in time");
            }

            return ValidationResult.Ok();
        }
    }
}
