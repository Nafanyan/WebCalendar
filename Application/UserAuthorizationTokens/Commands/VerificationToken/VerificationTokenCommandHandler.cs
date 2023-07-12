using Application.Interfaces;
using Application.Result;
using Application.Validation;

namespace Application.UserAuthorizationTokens.Commands.VerificationToken
{
    public class VerificationTokenCommandHandler : ICommandHandler<VerificationTokenCommand>
    {
        private readonly IAsyncValidator<VerificationTokenCommand> _userAuthorizationTokenValidator;

        public VerificationTokenCommandHandler(IAsyncValidator<VerificationTokenCommand> validator)
        {
            _userAuthorizationTokenValidator = validator;
        }

        public async Task<CommandResult> HandleAsync(VerificationTokenCommand command)
        {
            ValidationResult validationResult = await _userAuthorizationTokenValidator.ValidationAsync(command);

            return new CommandResult(validationResult);
        }
    }
}
