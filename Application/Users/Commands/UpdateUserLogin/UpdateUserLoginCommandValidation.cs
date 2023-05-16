using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandValidation : IValidator<UpdateUserLoginCommand>, IAsyncValidator<UpdateUserLoginCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserLoginCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validation(UpdateUserLoginCommand command)
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
        public async Task<ValidationResult> AsyncValidation(UpdateUserLoginCommand command)
        {
            string error = "No errors";
            ValidationResult validationResult = new ValidationResult(false, error);

            User user = await _userRepository.GetById(command.Id);
            if (user == null)
            {
                error = "There is no user with this id";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            IReadOnlyList<User> users = await _userRepository.GetAll();
            if (users.Where(u => u.Login == command.Login).FirstOrDefault() != null)
            {
                error = "A user with this login already exists";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            if (user.PasswordHash != command.PasswordHash)
            {
                error = "The entered password does not match the current one";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            return validationResult;
        }
    }
}
