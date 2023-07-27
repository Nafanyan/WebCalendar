using Application.Validation;
using Application.Entities;
using Application.Repositories;

namespace Application.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandValidator : IAsyncValidator<UpdateUserLoginCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserLoginCommandValidator( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync( UpdateUserLoginCommand command )
        {
            if( command.Login == null || command.Login == String.Empty )
            {
                return ValidationResult.Fail( "Логин не может быть пустым или null" );
            }

            if( !await _userRepository.ContainsAsync( user => user.Id == command.Id ) )
            {
                return ValidationResult.Fail( "Пользователя с таким id нет" );
            }

            if( await _userRepository.ContainsAsync( user => user.Login == command.Login ) )
            {
                return ValidationResult.Fail( "Пользователь с таким логином уже существует" );
            }

            User user = await _userRepository.GetByIdAsync( command.Id );
            if( user.PasswordHash != command.PasswordHash )
            {
                return ValidationResult.Fail( "Введенный пароль не совпадает с текущим" );
            }

            return ValidationResult.Ok();
        }
    }
}
