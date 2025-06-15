using Application.Validation;
using Domain.Entities;
using Application.Repositories;

namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : IAsyncValidator<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandValidator( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync( DeleteUserCommand command )
        {
            if( !await _userRepository.ContainsAsync( user => user.Id == command.Id ) )
            {
                return ValidationResult.Fail( "Пользователя с таким id нет" );
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
