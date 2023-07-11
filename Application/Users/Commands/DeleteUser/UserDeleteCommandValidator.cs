using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.DeleteUser
{
    public class UserDeleteCommandValidator : IAsyncValidator<UserDeleteCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserDeleteCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidationAsync(UserDeleteCommand command)
        {
            if (!await _userRepository.ContainsAsync(user => user.Id == command.Id))
            {
                return ValidationResult.Fail("There is no user with this id");
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
