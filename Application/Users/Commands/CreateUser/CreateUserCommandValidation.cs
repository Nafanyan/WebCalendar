using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.CreateUser
{
    class CreateUserCommandValidation : IValidator<CreateUserCommand>, IAsyncValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validation(CreateUserCommand command)
        {
            string error = "No errors";
            ValidationResult validationResult = new ValidationResult(false, error);

            if (command.Login == null)
            {
                error = "The login cannot be empty/cannot be null";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            validationResult = AsyncValidation(command).Result;
            if (validationResult.IsFail)
            {
                return validationResult;
            }

            return validationResult;
        }
        public async Task<ValidationResult> AsyncValidation(CreateUserCommand command)
        {
            string error = "No errors";
            IReadOnlyList<User> users = await _userRepository.GetAll();

            if (users.Where(u => u.Login == command.Login).FirstOrDefault() != null)
            {
                error = "A user with this login already exists";
                return new ValidationResult(true, error);
            }
            return new ValidationResult(false, error);
        }
    }
}
