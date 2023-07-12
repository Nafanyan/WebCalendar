using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.CreateUser
{
    public class UserCreateCommandValidator : IAsyncValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserCreateCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(CreateUserCommand command)
        {
            if (command.Login == null)
            {
                return ValidationResult.Fail("The login cannot be empty/cannot be null");
            }

            if (await _userRepository.ContainsAsync(user => user.Login == command.Login))
            {
                return ValidationResult.Fail("A user with this login already exists");
            }
            return ValidationResult.Ok();
        }
    }
}
