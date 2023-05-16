using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidation : IValidator<DeleteUserCommand>, IAsyncValidator<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validation(DeleteUserCommand command)
        {
            string error = "No errors";
            ValidationResult validationResult = new ValidationResult(false, error);

            if (_userRepository.GetById(command.Id) == null)
            {
                error = "There is no user with this id";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            validationResult = AsyncValidation(command).Result;
            if (validationResult.IsFail)
            {
                return validationResult;
            }

            return new ValidationResult(false, error);
        }
        public async Task<ValidationResult> AsyncValidation(DeleteUserCommand command)
        {
            string error = "No errors";
            User user = await _userRepository.GetById(command.Id);

            if (user.PasswordHash != command.PasswordHash)
            {
                error = "The entered password does not match the current one";
                return new ValidationResult(true, error);
            }
            return new ValidationResult(false, error);
        }
    }
}
