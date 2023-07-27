using Application.Validation;
using Application.Entities;
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
                return ValidationResult.Fail( "There is no user with this id" );
            }

            User user = await _userRepository.GetByIdAsync( command.Id );
            if( user.PasswordHash != command.PasswordHash )
            {
                return ValidationResult.Fail( "The entered password does not match the current one" );
            }

            return ValidationResult.Ok();
        }
    }
}
