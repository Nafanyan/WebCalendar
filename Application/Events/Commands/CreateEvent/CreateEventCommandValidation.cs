using Application.Validation;

namespace Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidation : IAsyncValidator<CreateEventCommand>
    {
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

            if (command.StartEvent > command.EndEvent)
            {
                return ValidationResult.Fail("The start date cannot be later than the end date");
            }

            return ValidationResult.Ok();
        }
    }
}
