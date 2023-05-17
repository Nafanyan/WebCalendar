using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandValidation : IAsyncValidator<UpdateUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserPasswordCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> Validation(UpdateUserPasswordCommand command)
        {
            string error;
            if (!await _userRepository.Contains(command.Id))
            {
                error = "There is no user with this id";
                return ValidationResult.Fail(error);
            }

            User user = await _userRepository.GetById(command.Id);
            if (user.PasswordHash != command.OldPasswordHash)
            {
                error = "The entered password does not match the current one";
                return ValidationResult.Fail(error);
            }

            return ValidationResult.Ok();
        }
    }
}
