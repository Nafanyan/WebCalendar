using Application.Validation;
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
                return ValidationResult.Fail("The start date cannot be empty/cannot be null");
            }

            if (command.EndEvent == null)
            {
                return ValidationResult.Fail("The end date cannot be empty/cannot be null");
            }

            DateTime startEvent;
            if (!DateTime.TryParse(command.StartEvent, out startEvent))
            {
                return ValidationResult.Fail("Error in writing the start date of the event");
            }

            DateTime endEvent;
            if (!DateTime.TryParse(command.StartEvent, out endEvent))
            {
                return ValidationResult.Fail("Error in writing the end date of the event");
            }

            if (startEvent > endEvent)
            {
                return ValidationResult.Fail("The start date cannot be later than the end date");
            }

            if (startEvent.ToShortDateString() != endEvent.ToShortDateString())
            {
                return ValidationResult.Fail("The event must occur within one day");
            }

            if (!await _eventRepository.ContainsAsync(command.UserId, startEvent, endEvent))
            {
                return ValidationResult.Fail("An event with such a time does not exist");
            }

            return ValidationResult.Ok();
        }
    }
}
