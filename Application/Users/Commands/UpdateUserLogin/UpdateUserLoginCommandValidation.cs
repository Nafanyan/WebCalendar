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
            string error;
            if (command.Login == null)
            {
                error = "The login cannot be empty/cannot be null";
                return ValidationResult.Fail(error);
            }

            if (!await _userRepository.Contains(command.Id))
            {
                error = "There is no user with this id";
                return ValidationResult.Fail(error);
            }

            IReadOnlyList<User> users = await _userRepository.GetAll();
            if (users.Where(u => u.Login == command.Login).FirstOrDefault() != null)
            {
                error = "A user with this login already exists";
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
