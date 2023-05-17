using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.CreateUser
{
    class CreateUserCommandValidation : IAsyncValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> Validation(CreateUserCommand command)
        {
            if (command.Login == null)
            {
                return ValidationResult.Fail("The login cannot be empty/cannot be null");
            }

            IReadOnlyList<User> users = await _userRepository.GetAll();

            if (users.Where(u => u.Login == command.Login).FirstOrDefault() != null)
            {
                return ValidationResult.Fail("A user with this login already exists");
            }
            return ValidationResult.Ok();
        }
    }
}
