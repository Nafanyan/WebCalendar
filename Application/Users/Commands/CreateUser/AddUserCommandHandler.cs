using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.CreateUser
{
    public interface IAddUserCommandHandler
    {
        void Execute(AddUserCommand addUserCommand);
    }

    public class AddUserCommandHandler : BaseUserUseCase, IAddUserCommandHandler
    {
        public AddUserCommandHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        public void Execute(AddUserCommand addUserCommand)
        {
            CommandValidation(addUserCommand);

            User user = new User(addUserCommand.Login, addUserCommand.PasswordHash);
            _userRepository.Add(user);
        }
        private void CommandValidation(AddUserCommand addUserCommand)
        {
            _validationUser.LoginСorrectness(addUserCommand.Login);
        }
    }
}
