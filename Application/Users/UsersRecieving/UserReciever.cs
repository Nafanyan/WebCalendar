using WebCalendar.Domain.Users;

namespace WebCalendar.Application.Users.UsersRecieving
{
    public interface IUserReciever
    {
        User GetUser(long id);
    }

    public class UserReciever : BaseUserUsCase, IUserReciever
    {
        public UserReciever(IUserRepository userRepository) : base(userRepository)
        {
        }

        public User GetUser(long id)
        {
            ValidationCheck(id);

            return _userRepository.GetById(id);
        }

        private void ValidationCheck(long id)
        {
            _validationUser.ValueNotFound(id);
        }
    }
}
