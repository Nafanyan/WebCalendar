using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UserAuthorizationTokens.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : IAsyncValidator<AuthenticateUserCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;
        private readonly IUserRepository _userRepository;

        public AuthenticateUserCommandValidator(
            IUserAuthorizationTokenRepository userAuthorizationTokenRepository, 
            IUserRepository userRepository)
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(AuthenticateUserCommand command)
        {
            if (command.Login == null)
            {
                return ValidationResult.Fail("The login cannot be empty/cannot be null");
            }

            if (!await _userRepository.ContainsAsync(user => user.Login == command.Login))
            {
                return ValidationResult.Fail("Invalid username or password");
            }

            User user = await _userRepository.GetByLoginAsync(command.Login);
            if (user.PasswordHash != command.PasswordHash)
            {
                return ValidationResult.Fail("Invalid username or password");
            }

            return ValidationResult.Ok();
        }
    }
}
