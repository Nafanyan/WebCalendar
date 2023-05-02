using WebCalendar.Domain.Users;
using WebCalendar.Domain.Validation.UserValidation;

namespace WebCalendar.Application.Users
{
    public class BaseUserUsCase
    {
        protected readonly IUserRepository _userRepository;
        protected readonly ValidationUser _validationUser;

        public BaseUserUsCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _validationUser = new ValidationUser(_userRepository);
        }
    }
}
