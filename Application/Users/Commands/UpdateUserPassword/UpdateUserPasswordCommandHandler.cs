using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Commands.UpdateUserPassword
{
    public interface IUpdateUserPasswordCommandHandler
    {
        public void Execute(UpdateUserPasswordCommand updateUserPasswordCommand);
    }
    public class UpdateUserPasswordCommandHandler : BaseUserUseCase, IUpdateUserPasswordCommandHandler
    {
        public UpdateUserPasswordCommandHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        public void Execute(UpdateUserPasswordCommand updateUserPasswordCommand)
        {
            CommandValidation(updateUserPasswordCommand);

            User user = _userRepository.GetById(updateUserPasswordCommand.Id).Result;
            user.SetPasswordHash(updateUserPasswordCommand.NewPasswordHash);
        }
        public void CommandValidation(UpdateUserPasswordCommand updateUserPasswordCommand)
        {
            _validationUser.ValueNotFound(updateUserPasswordCommand.Id);
            _validationUser.PasswordHashVerification(updateUserPasswordCommand.Id, updateUserPasswordCommand.OldPasswordHash);
        }
    }
}
