using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UserAuthorizationTokens
{
    public class AuthorizationUserQueryValidator : IAsyncValidator<UserAuthorizationQuery>
    {
        private readonly UserAuthorizationTokenRepository _userAuthorizationRepository;
        private readonly IUserRepository _userRepository;

        public AuthorizationUserQueryValidator(UserAuthorizationTokenRepository userAuthorizationRepository, IUserRepository userRepository)
        {
            _userAuthorizationRepository = userAuthorizationRepository;
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(UserAuthorizationQuery query)
        {
            if (query.Login == null)
            {
                return ValidationResult.Fail("The login cannot be empty/cannot be null");
            }

            if (!await _userRepository.ContainsAsync(user => user.Login == query.Login))
            {
                return ValidationResult.Fail("Invalid username or password");
            }

            User user = await _userRepository.GetByIdAsync(query.UserId);
            if (user.PasswordHash != query.PasswordHash)
            {
                return ValidationResult.Fail("Invalid username or password");
            }

            if (query.RefreshToken == (await _userAuthorizationRepository.GetTokenAsync(query.UserId)).RefreshToken)
            {
                return ValidationResult.Fail("Your token is outdated or invalid");
            }

            return ValidationResult.Ok();
        }
    }
}
