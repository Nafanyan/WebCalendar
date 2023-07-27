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
                return ValidationResult.Fail( "The login cannot be empty/cannot be null" );
            }

            if( command.Login.Length > 28 )
            {
                return ValidationResult.Fail( "Login must be less than 28 characters" );
            }

            if( await _userRepository.ContainsAsync( user => user.Login == command.Login ) )
            {
                return ValidationResult.Fail( "A user with this login already exists" );
            }
            return ValidationResult.Ok();
        }
    }
}
