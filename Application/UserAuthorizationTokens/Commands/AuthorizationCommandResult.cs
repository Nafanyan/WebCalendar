using Application.Validation;

namespace Application.UserAuthorizationTokens.Commands
{
    public class AuthorizationCommandResult<TCommandResultData> where TCommandResultData : class
    {
        public ValidationResult ValidationResult { get; private set; }
        public TCommandResultData ObjResult { get; private set; }

        public AuthorizationCommandResult(TCommandResultData objResult)
        {
            ObjResult = objResult;
            ValidationResult = ValidationResult.Ok();
        }

        public AuthorizationCommandResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
