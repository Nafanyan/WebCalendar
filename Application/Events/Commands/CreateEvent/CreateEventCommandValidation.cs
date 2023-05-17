using Application.Validation;

namespace Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidation : IAsyncValidator<CreateEventCommand>
    {
        public async Task<ValidationResult> Validation(CreateEventCommand command)
        {
            string error;
            if (command.Name == null)
            {
                error = "The name of event cannot be empty/cannot be null";
                return ValidationResult.Fail(error);
            }

            if (command.StartEvent == null)
            {
                error = "The start date cannot be empty/cannot be null";
                return ValidationResult.Fail(error);
            }

            if (command.StartEvent == null)
            {
                error = "The end date cannot be empty/cannot be null";
                return ValidationResult.Fail(error);
            }

            if (command.StartEvent > command.EndEvent)
            {
                error = "The start date cannot be later than the end date";
                return ValidationResult.Fail(error);
            }

            return ValidationResult.Ok();
        }
    }
}
