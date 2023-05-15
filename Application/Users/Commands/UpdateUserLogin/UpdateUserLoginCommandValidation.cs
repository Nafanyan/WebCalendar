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

        public string Validation(UpdateUserLoginCommand command)
        {
            if (_userRepository.GetById(command.Id) == null)
            {
               return "There is no user with this id";
            }

            if (command.Login == null)
            {
                return "The login cannot be empty/cannot be null";
            }

            if (_userRepository.GetAll().Result.Where(u => u.Login == command.Login)
                .FirstOrDefault() != null)
            {
                return "A user with this login already exists";
            }

            User user = _userRepository.GetById(command.Id).Result;
            if (user.PasswordHash != command.PasswordHash)
            {
                return "The entered password does not match the current one";
            }

            return "Ok";
        }
    }
}
