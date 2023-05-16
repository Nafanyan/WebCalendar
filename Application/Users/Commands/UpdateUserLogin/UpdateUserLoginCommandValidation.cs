using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandValidation : IValidation<UpdateUserLoginCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserLoginCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validation(UpdateUserLoginCommand command)
        {
            string error = "No errors";
            if (_userRepository.GetById(command.Id) == null)
            {
                error = "There is no user with this id";
                return new ValidationResult(true, error);
            }

            if (command.Login == null)
            {
                error = "The login cannot be empty/cannot be null";
                return new ValidationResult(true, error);
            }

            if (_userRepository.GetAll().Result.Where(u => u.Login == command.Login)
                .FirstOrDefault() != null)
            {
                error = "A user with this login already exists";
                return new ValidationResult(true, error);
            }

            User user = _userRepository.GetById(command.Id).Result;
            if (user.PasswordHash != command.PasswordHash)
            {
                error = "The entered password does not match the current one";
                return new ValidationResult(true, error);
            }

            return new ValidationResult(false, error);
        }
    }
}
