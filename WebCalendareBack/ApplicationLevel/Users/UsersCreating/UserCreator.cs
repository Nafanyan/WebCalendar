using WebCalendar.Application.Events.Commands;
using WebCalendar.Application.Users.Commands;
using WebCalendar.Domain.Users;
using WebCalendar.Domain.Validation.EventValidation;

namespace WebCalendar.Application.Users.UsersCreating
{
    public interface IUserCreator
    {
        long Create(AddUserCommand addUserCommand);
    }

    public class UserCreator : BaseUserUsCase, IUserCreator
    {
        public UserCreator(IUserRepository userRepository) : base(userRepository)
        {
        }

        public long Create(AddUserCommand addUserCommand)
        {
            ValidationCheck(addUserCommand);

            User user = new User(addUserCommand.Login, addUserCommand.PasswordHash);
            return _userRepository.Add(user);
        }

        private void ValidationCheck(AddUserCommand addUserCommand)
        {
            _validationUser.CheckingLogin(addUserCommand.Login);
        }
    }
}
