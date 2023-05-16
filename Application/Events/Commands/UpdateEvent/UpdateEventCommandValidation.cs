using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidation : IValidator<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandValidation(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public ValidationResult Validation(UpdateEventCommand command)
        {
            string error = "No errors";
            if (command.StartEvent == null)
            {
                error = "The start date cannot be empty/cannot be null";
                return new ValidationResult(true, error);
            }

            if (command.EndEvent == null)
            {
                error = "The end date cannot be empty/cannot be null";
                return new ValidationResult(true, error);
            }

            if (command.StartEvent > command.EndEvent)
            {
                error = "The start date cannot be later than the end date";
                return new ValidationResult(true, error);
            }

            EventPeriod eventPeriod = new EventPeriod(command.StartEvent, command.EndEvent);
            if (_eventRepository.GetEvent(command.UserId, eventPeriod).Result == null)
            {
                error = "An event with such a time does not exist";
                return new ValidationResult(true, error);
            }

            return new ValidationResult(false, error);

        }
    }
}
