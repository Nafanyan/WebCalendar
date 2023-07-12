using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UserAuthorizationTokens.Commands.CreateToken
{
    public class TokenCreateCommandValidator : IAsyncValidator<CreateTokenCommand>
    {
        private readonly IUserAuthorizationTokenRepository _userAuthorizationTokenRepository;
        private readonly IUserRepository _userRepository;

        public TokenCreateCommandValidator(
            IUserAuthorizationTokenRepository userAuthorizationTokenRepository, 
            IUserRepository userRepository)
        {
            _userAuthorizationTokenRepository = userAuthorizationTokenRepository;
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(CreateTokenCommand command)
        {
            if (command.Login == null)
            {
                return ValidationResult.Fail("The login cannot be empty/cannot be null");
            }

            if (!await _userRepository.ContainsAsync(user => user.Login == command.Login))
            {
                return ValidationResult.Fail("Invalid username or password");
            }

            User user = await _userRepository.GetByIdAsync(command.UserId);
            if (user.PasswordHash != command.PasswordHash)
            {
                return ValidationResult.Fail("Invalid username or password");
            }

            return ValidationResult.Ok();
        }
    }
}
