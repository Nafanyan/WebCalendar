using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandValidation : IAsyncValidator<UpdateUserLoginCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserLoginCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> Validation(UpdateUserLoginCommand command)
        {
            if (command.Login == null)
            {
                return ValidationResult.Fail("The login cannot be empty/cannot be null");
            }

            if (await _userRepository.Contains(user => user.Id != command.Id))
            {
                return ValidationResult.Fail("There is no user with this id");
            }

            if (await _userRepository.Contains(user => user.Login == command.Login))
            {
                return ValidationResult.Fail("A user with this login already exists");
            }

            User user = await _userRepository.GetById(command.Id);
            if (user.PasswordHash != command.PasswordHash)
            {
                return ValidationResult.Fail("The entered password does not match the current one");
            }

            return ValidationResult.Ok();
        }
    }
}
