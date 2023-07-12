using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UserAuthorizationTokens.Commands.VerificationToken
{
    public class VerificationTokenCommandValidator : IAsyncValidator<VerificationTokenCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;

        public VerificationTokenCommandValidator(IUserAuthorizationTokenRepository userAuthorizationTokenRepository)
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
        }

        public async Task<ValidationResult> ValidationAsync(VerificationTokenCommand command)
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
