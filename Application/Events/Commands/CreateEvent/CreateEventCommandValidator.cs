using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : IAsyncValidator<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandValidator(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ValidationResult> ValidationAsync(CreateEventCommand command)
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

            DateTime startEvent;
            if (!DateTime.TryParse(command.StartEvent, out startEvent))
            {
                return ValidationResult.Fail("Error in writing the start date of the event");
            }

            DateTime endEvent;
            if (!DateTime.TryParse(command.EndEvent, out endEvent))
            {
                return ValidationResult.Fail("Error in writing the end date of the event");
            }
            if (startEvent.ToShortDateString() != endEvent.ToShortDateString())
            {
                return ValidationResult.Fail("The event must occur within one day");
            }

            if (startEvent > endEvent)
            {
                return ValidationResult.Fail("The start date cannot be later than the end date");
            }
            if (await _eventRepository.ContainsAsync(command.UserId, startEvent, endEvent))
            {
                return ValidationResult.Fail("This event is superimposed on the existing event in time");
            }

            return ValidationResult.Ok();
        }
    }
}
