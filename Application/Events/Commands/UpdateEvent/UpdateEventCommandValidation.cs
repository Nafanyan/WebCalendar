using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidation : IAsyncValidator<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandValidation(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ValidationResult> Validation(UpdateEventCommand command)
        {
            string error;
            if (command.StartEvent == null)
            {
                error = "The start date cannot be empty/cannot be null";
                return ValidationResult.Fail(error);
            }

            if (command.EndEvent == null)
            {
                error = "The end date cannot be empty/cannot be null";
                return ValidationResult.Fail(error);
            }

            if (command.StartEvent > command.EndEvent)
            {
                error = "The start date cannot be later than the end date";
                return ValidationResult.Fail(error);
            }

            EventPeriod eventPeriod = new EventPeriod(command.StartEvent, command.EndEvent);
            if (!await _eventRepository.Contains(command.UserId, eventPeriod))
            {
                error = "An event with such a time does not exist";
                return ValidationResult.Fail(error);
            }

            return ValidationResult.Ok();
        }
    }
}
