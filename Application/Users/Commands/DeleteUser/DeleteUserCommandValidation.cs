using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidation : IAsyncValidator<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> Validation(DeleteUserCommand command)
        {
            if (_userRepository.GetById(command.Id) == null)
            {
                return ValidationResult.Fail("There is no user with this id");
            }

            if (!await _userRepository.Contains(command.Id))
            {
                return ValidationResult.Fail("There is no user with this id");
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
