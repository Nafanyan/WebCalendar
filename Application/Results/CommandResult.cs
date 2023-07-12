using Application.Validation;

namespace Application.Result
{
    public class AuthorizationCommandResult
    {
        public ValidationResult ValidationResult { get; private set; }

        public AuthorizationCommandResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
