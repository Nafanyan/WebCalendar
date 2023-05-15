using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidation : IValidation<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Validation(DeleteUserCommand command)
        {
            if (_userRepository.GetById(command.Id) == null)
            {
                return "There is no user with this id";
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
