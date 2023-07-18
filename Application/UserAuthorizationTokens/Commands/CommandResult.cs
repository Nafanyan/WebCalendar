using Application.Validation;

namespace Application.UserAuthorizationTokens.Commands
{
    public class CommandResult<TCommandResultData> where TCommandResultData : class
    {
        public ValidationResult ValidationResult { get; private set; }
        public TCommandResultData ObjResult { get; private set; }

        public CommandResult(TCommandResultData objResult)
        {
            ObjResult = objResult;
            ValidationResult = ValidationResult.Ok();
        }

        public CommandResult(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
