using Application.Validation;
using Application.Entities;
using Application.Repositories;

namespace Application.UserAuthorizationTokens.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : IAsyncValidator<AuthenticateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public AuthenticateUserCommandValidator( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync( AuthenticateUserCommand command )
        {
            if( command.Login == null || command.Login == String.Empty )
            {
                return ValidationResult.Fail( "Логин не может быть пустым или null" );
            }

            if( !await _userRepository.ContainsAsync( user => user.Login == command.Login && user.PasswordHash == command.PasswordHash ) )
            {
                return ValidationResult.Fail( "Неверное имя пользователя или пароль" );
            }

            return ValidationResult.Ok();
        }
    }
}
