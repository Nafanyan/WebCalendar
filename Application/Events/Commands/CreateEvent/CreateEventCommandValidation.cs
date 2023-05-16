using Application.Validation;

namespace Application.Events.Commands.CreateEvent
{
    class CreateEventCommandValidation : IValidation<CreateEventCommand>
    {
        public ValidationResult Validation(CreateEventCommand command)
        {
            string error = "No errors";
            if (command.Name == null)
            {
                error = "The name of event cannot be empty/cannot be null";
                return new ValidationResult(true, error);
            }

            if (command.StartEvent == null)
            {
                error = "The start date cannot be empty/cannot be null";
                return new ValidationResult(true, error);
            }

            if (command.StartEvent == null)
            {
                error = "The end date cannot be empty/cannot be null";
                return new ValidationResult(true, error);
            }

            if (command.StartEvent > command.EndEvent)
            {
                error = "The start date cannot be later than the end date";
                return new ValidationResult(true, error);
            }

            return new ValidationResult(false, error);
        }
    }
}
