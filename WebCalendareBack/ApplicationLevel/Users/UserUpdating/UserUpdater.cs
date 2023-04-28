using WebCalendar.Application.Users.Commands;
using WebCalendar.Domain.Users;

namespace WebCalendar.Application.Users.UserUpdating
{
    public interface IUserUpdater
    {
        void UpdateLogin(UpdateUserCommand updateUserCommand);
        bool UpdatePasswordHash(long id, string oldPasswordHash, string newPasswordHash);
    }
    public class UserUpdater : BaseUserUsCase, IUserUpdater
    {
        public UserUpdater(IUserRepository userRepository) : base(userRepository)
        {
        }

        public void UpdateLogin(UpdateUserCommand updateUserCommand)
        {
            ValidationCheck(updateUserCommand);

            User user = _userRepository.GetById(updateUserCommand.Id);

            if (user.CheckPasswordHash(updateUserCommand.PasswordHash))
            {
                user.UpdateLogin(updateUserCommand.Login);
                _userRepository.Update(user);
            }

        }
        public bool UpdatePasswordHash(long id, string oldPasswordHash, string newPasswordHash)
        {
            User user = _userRepository.GetById(id);
            return user.UpdatePasswordHash(oldPasswordHash, newPasswordHash);
        }

        private void ValidationCheck(UpdateUserCommand updateUserCommand)
        {
            _validationUser.CheckingContentInRepository(updateUserCommand.Id);
            _validationUser.CheckingLogin(updateUserCommand.Login);
        }
    }
}
