using Application.Validation;
using Domain.Repositories;

namespace Application.Users.Commands.CreateUser
{
    class CreateUserCommandValidation : IValidation<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validation(CreateUserCommand command)
        {
            string error = "No errors";
            if (command.Login == null)
            {
                error = "The login cannot be empty/cannot be null";
                return new ValidationResult(true, error);
            }

            if (_userRepository.GetAll().Result.Where(u => u.Login == command.Login).
                FirstOrDefault() != null)
            {
                error = "A user with this login already exists";
                return new ValidationResult(true, error);
            }

            return new ValidationResult(false, error);
        }
    }
}
