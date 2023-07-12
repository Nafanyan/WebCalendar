using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandValidator : IAsyncValidator<UpdateUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserPasswordCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(UpdateUserPasswordCommand command)
        {
            if (!await _userRepository.ContainsAsync(user => user.Id == command.Id))
            {
                return ValidationResult.Fail("There is no user with this id");
            }

            User user = await _userRepository.GetByIdAsync(command.Id);
            if (user.PasswordHash != command.OldPasswordHash)
            {
                return ValidationResult.Fail("The entered password does not match the current one");
            }

            return ValidationResult.Ok();
        }
    }
}
