using WebCalendar.Domain.Users;
using WebCalendar.Domain.Validation.UserValidation;

namespace WebCalendar.Application.Users
{
    public class BaseUserUsCase
    {
        protected readonly IUserRepository _userRepository;
        protected readonly UserValidation _validationUser;

        public BaseUserUsCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _validationUser = new UserValidation(_userRepository);
        }
    }
}
