using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidation : IValidator<UpdateEventCommand>, IAsyncValidator<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandValidation(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public ValidationResult Validation(UpdateEventCommand command)
        {
            string error = "No errors";
            ValidationResult validationResult = new ValidationResult(false, error);

            if (command.StartEvent == null)
            {
                error = "The start date cannot be empty/cannot be null";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            if (command.EndEvent == null)
            {
                error = "The end date cannot be empty/cannot be null";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            if (command.StartEvent > command.EndEvent)
            {
                error = "The start date cannot be later than the end date";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            validationResult = AsyncValidation(command).Result;
            if (validationResult.IsFail)
            {
                return validationResult;
            }

            return validationResult;
        }
        public async Task<ValidationResult> AsyncValidation(UpdateEventCommand command)
        {
            string error = "No errors";
            EventPeriod eventPeriod = new EventPeriod(command.StartEvent, command.EndEvent);

            if (await _eventRepository.GetEvent(command.UserId, eventPeriod) == null)
            {
                error = "An event with such a time does not exist";
                return new ValidationResult(true, error);
            }
            return new ValidationResult(false, error);
        }
    }
}
