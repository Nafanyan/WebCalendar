using Application.Validation;

namespace Application.Result
{
    public class CommandResult
    {
        public ValidationResult ValidationResult { get; private set; }

        public CommandResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
