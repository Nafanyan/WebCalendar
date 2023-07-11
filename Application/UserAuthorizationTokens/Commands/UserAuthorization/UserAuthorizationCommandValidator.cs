using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UserAuthorizationTokens.Commands.UserAuthorization
{
    public class AuthorizationUserQueryValidator : IAsyncValidator<UserAuthorizationCommand>
    {
        private readonly UserAuthorizationTokenRepository _userAuthorizationRepository;
        private readonly IUserRepository _userRepository;

        public AuthorizationUserQueryValidator(UserAuthorizationTokenRepository userAuthorizationRepository, IUserRepository userRepository)
        {
            _userAuthorizationRepository = userAuthorizationRepository;
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(UserAuthorizationCommand query)
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

            return ValidationResult.Ok();
        }
    }
}
