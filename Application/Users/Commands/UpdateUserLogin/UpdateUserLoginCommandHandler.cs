using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserLogin
{
    public interface IUpdateUserLoginCommandHandler
    {
        void Execute(UpdateUserLoginCommand updateUserLoginCommand);
    }
    public class UpdateUserLoginCommandHandler : BaseUserUseCase, IUpdateUserLoginCommandHandler
    {
        public UpdateUserLoginCommandHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        public void Execute(UpdateUserLoginCommand updateUserLoginCommand)
        {
            CommandValidation(updateUserLoginCommand);

            User user = _userRepository.GetById(updateUserLoginCommand.Id).Result;
            user.SetLogin(updateUserLoginCommand.Login);
        }
        private void CommandValidation(UpdateUserLoginCommand updateUserLoginCommand)
        {
            _validationUser.ValueNotFound(updateUserLoginCommand.Id);
            _validationUser.LoginСorrectness(updateUserLoginCommand.Login);
            _validationUser.PasswordHashVerification(updateUserLoginCommand.Id, updateUserLoginCommand.PasswordHash);
        }
    }
}
