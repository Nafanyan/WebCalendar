using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UserAuthorizationTokens.Commands.TokenVerification
{
    public class TokenVerificationCommandValidator : IAsyncValidator<TokenVerificationCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;

        public TokenVerificationCommandValidator(IUserAuthorizationTokenRepository userAuthorizationTokenRepository)
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
        }

        public async Task<ValidationResult> ValidationAsync(TokenVerificationCommand command)
        {
            UserAuthorizationToken token = await _userAuthorizationTokenRepository.GetTokenByUserIdAsync(command.UserId);
            if (DateTime.Now > token.ExpiryDate)
            {
                return ValidationResult.Fail("Token expired");
            }

            if (token.RefreshToken != command.RefreshToken)
            {
                return ValidationResult.Fail("Token is not valid");
            }

            return ValidationResult.Ok();
        }
    }
}
