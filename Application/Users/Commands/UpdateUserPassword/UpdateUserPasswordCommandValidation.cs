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
            if (await _userRepository.Contains(user => user.Id != command.Id))
            {
                return ValidationResult.Fail("There is no user with this id");
            }

            User user = await _userRepository.GetById(command.Id);
            if (user.PasswordHash != command.OldPasswordHash)
            {
                return ValidationResult.Fail("The entered password does not match the current one");
            }

            return ValidationResult.Ok();
        }
    }
}
