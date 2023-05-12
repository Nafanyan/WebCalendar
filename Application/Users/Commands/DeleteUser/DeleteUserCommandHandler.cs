using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.DeleteUser
{
    public interface IDeleteUserCommandHandler
    {
        void Execute(DeleteUserCommand deleteUserCommand);
    }
    public class DeleteUserCommandHandler : BaseUserUseCase, IDeleteUserCommandHandler
    {
        public DeleteUserCommandHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        public void Execute(DeleteUserCommand deleteUserCommand)
        {
            CommandValidation(deleteUserCommand);

            User user = _userRepository.GetById(deleteUserCommand.Id).Result;
            _userRepository.Delete(user);
        }
        private void CommandValidation(DeleteUserCommand deleteUserCommand)
        {
            _validationUser.ValueNotFound(deleteUserCommand.Id);
            _validationUser.PasswordHashVerification(deleteUserCommand.Id, deleteUserCommand.PasswordHash);
        }
    }
}
