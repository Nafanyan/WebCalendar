using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandValidator : IAsyncValidator<UpdateUserLoginCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserLoginCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(UpdateUserLoginCommand command)
        {
            if (command.Login == null || command.Login == String.Empty)
            {
                return ValidationResult.Fail("The login cannot be empty/cannot be null");
            }

            if (!await _userRepository.ContainsAsync(user => user.Id == command.Id))
            {
                return ValidationResult.Fail("There is no user with this id");
            }

            if (await _userRepository.ContainsAsync(user => user.Login == command.Login))
            {
                return ValidationResult.Fail("A user with this login already exists");
            }

            User user = await _userRepository.GetByIdAsync(command.Id);
            if (user.PasswordHash != command.PasswordHash)
            {
                return ValidationResult.Fail("The entered password does not match the current one");
            }

            return ValidationResult.Ok();
        }
    }
}
