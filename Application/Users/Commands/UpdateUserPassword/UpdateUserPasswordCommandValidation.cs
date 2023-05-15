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

        public string Validation(UpdateUserPasswordCommand command)
        {
            if (_userRepository.GetById(command.Id) == null)
            {
                return "There is no user with this id";
            }

            User user = _userRepository.GetById(command.Id).Result;
            if (user.PasswordHash != command.OldPasswordHash)
            {
                return "The entered password does not match the current one";
            }

            return "Ok";
        }
    }
}
