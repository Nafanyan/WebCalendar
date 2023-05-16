using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandValidation : IValidator<UpdateUserPasswordCommand>, IAsyncValidator<UpdateUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserPasswordCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validation(UpdateUserPasswordCommand command)
        {
            string error = "No errors";
            ValidationResult validationResult = new ValidationResult(false, error);

            validationResult = AsyncValidation(command).Result;
            if (validationResult.IsFail)
            {
                return validationResult;
            }

            return validationResult;
        }
        public async Task<ValidationResult> AsyncValidation(UpdateUserPasswordCommand command)
        {
            string error = "No errors";
            ValidationResult validationResult = new ValidationResult(false, error);

            if (await _userRepository.GetById(command.Id) == null)
            {
                error = "There is no user with this id";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            User user = await _userRepository.GetById(command.Id);
            if (user.PasswordHash != command.OldPasswordHash)
            {
                error = "The entered password does not match the current one";
                validationResult = new ValidationResult(true, error);
                return validationResult;
            }

            return validationResult;
        }
    }
}
