using Application.Validation;
using Application.Entities;
using Application.Repositories;

namespace Application.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandValidator : IAsyncValidator<UpdateUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserPasswordCommandValidator( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync( UpdateUserPasswordCommand command )
        {
            if( !await _userRepository.ContainsAsync( user => user.Id == command.Id ) )
            {
                return ValidationResult.Fail( "Пользователя с таким id нет" );
            }

            User user = await _userRepository.GetByIdAsync( command.Id );
            if( user.PasswordHash != command.OldPasswordHash )
            {
                return ValidationResult.Fail( "Введенный пароль не совпадает с текущим" );
            }

            return ValidationResult.Ok();
        }
    }
}
