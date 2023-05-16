using Application.Validation;

namespace Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidation : IValidator<CreateEventCommand>
    {
        public ValidationResult Validation(CreateEventCommand command)
        {
            string error = "No errors";
            ValidationResult validationResult = new ValidationResult(false, error);

            if (command.Name == null)
            {
                error = "The name of event cannot be empty/cannot be null";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            if (command.StartEvent == null)
            {
                error = "The start date cannot be empty/cannot be null";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            if (command.StartEvent == null)
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

            return validationResult;
        }
    }
}
