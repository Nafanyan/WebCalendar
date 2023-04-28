using WebCalendar.Application.Users.Commands;
using WebCalendar.Domain.Users;

namespace WebCalendar.Application.Users.UserDeleting
{
    public interface IUserDeleter
    {
        void Delete(long id, string passwordHash);
    }
    public class UserDeleter : BaseUserUsCase, IUserDeleter
    {
        public UserDeleter(IUserRepository userRepository) : base(userRepository)
        {
        }

        public void Delete(long id, string passwordHash)
        {
            ValidationCheck(id);

            User user = _userRepository.GetById(id);

            if(user.CheckPasswordHash(passwordHash))
            {
                _userRepository.Delete(id);
            }
        }
        private void ValidationCheck(long id)
        {
            _validationUser.CheckingContentInRepository(id);
        }
    }
}
