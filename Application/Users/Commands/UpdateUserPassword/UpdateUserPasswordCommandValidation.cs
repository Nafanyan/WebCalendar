using Application.Validation;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandValidation : IValidation<UpdateUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserPasswordCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult Validation(UpdateUserPasswordCommand command)
        {
            string error = "No errors";
            if (_userRepository.GetById(command.Id) == null)
            {
                error = "There is no user with this id";
                return new ValidationResult(true, error);
            }

            User user = _userRepository.GetById(command.Id).Result;
            if (user.PasswordHash != command.OldPasswordHash)
            {
                error = "The entered password does not match the current one";
                return new ValidationResult(true, error);
            }

            return new ValidationResult(false, error);
        }
    }
}
