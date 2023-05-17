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
            string error;
            if (_userRepository.GetById(command.Id) == null)
            {
                error = "There is no user with this id";
                return ValidationResult.Fail(error);
            }

            if (!await _userRepository.Contains(command.Id))
            {
                error = "There is no user with this id";
                return ValidationResult.Fail(error);
            }

            User user = await _userRepository.GetById(command.Id);
            if (user.PasswordHash != command.PasswordHash)
            {
                error = "The entered password does not match the current one";
                return ValidationResult.Fail(error);
            }
            return ValidationResult.Ok();
        }
    }
}
