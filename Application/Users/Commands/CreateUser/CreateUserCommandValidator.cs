using Application.Validation;
using Application.Repositories;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : IAsyncValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandValidator( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync( CreateUserCommand command )
        {
            if( command.Login == null || command.Login == String.Empty )
            {
                return ValidationResult.Fail( "Логин не может быть пустым" );
            }

            if( command.Login.Length > 28 )
            {
                return ValidationResult.Fail( "Длина логина должна быть не более 28 символов" );
            }

            if( await _userRepository.ContainsAsync( user => user.Login == command.Login ) )
            {
                return ValidationResult.Fail( "Пользователь с таким логином уже существует" );
            }
            return ValidationResult.Ok();
        }
    }
}
