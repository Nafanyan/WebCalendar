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
            string error;

            if (command.Login == null)
            {
                error = "The login cannot be empty/cannot be null";
                return ValidationResult.Fail(error);
            }

            IReadOnlyList<User> users = await _userRepository.GetAll();

            if (users.Where(u => u.Login == command.Login).FirstOrDefault() != null)
            {
                error = "A user with this login already exists";
                return ValidationResult.Fail(error);
            }
            return ValidationResult.Ok();
        }
    }
}
