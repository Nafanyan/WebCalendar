using Application.Interfaces;
using Application.Result;
using Application.Validation;

namespace Application.UserAuthorizationTokens.Commands.TokenVerification
{
    public class TokenVerificationCommandHandler : ICommandHandler<TokenVerificationCommand>
    {
        private readonly IAsyncValidator<TokenVerificationCommand> _userAuthorizationTokenValidator;

        public TokenVerificationCommandHandler(IAsyncValidator<TokenVerificationCommand> validator)
        {
            _userAuthorizationTokenValidator = validator;
        }

        public async Task<CommandResult> HandleAsync(TokenVerificationCommand command)
        {
            ValidationResult validationResult = await _userAuthorizationTokenValidator.ValidationAsync(command);

            return new CommandResult(validationResult);
        }
    }
}
