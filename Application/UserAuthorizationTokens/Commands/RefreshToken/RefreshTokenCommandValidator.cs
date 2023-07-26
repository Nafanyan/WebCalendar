using Application.Validation;
using Application.Entities;
using Application.Repositories;

namespace Application.UserAuthorizationTokens.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : IAsyncValidator<RefreshTokenCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;

        public RefreshTokenCommandValidator(IUserAuthorizationTokenRepository userAuthorizationTokenRepository)
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
        }

        public async Task<ValidationResult> ValidationAsync(RefreshTokenCommand command)
        {
            UserAuthorizationToken token = await _userAuthorizationTokenRepository.GetByRefreshTokenAsync(command.RefreshToken);

            if (token is null)
            {
                return ValidationResult.Fail("Authorization required");
            }

            if (DateTime.Now > token.ExpiryDate)
            {
                return ValidationResult.Fail("Token expired");
            }

            return ValidationResult.Ok();
        }
    }
}
