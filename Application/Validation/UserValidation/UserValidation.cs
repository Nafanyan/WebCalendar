using Domain.Entities;
using Domain.Repositories;

namespace WebCalendar.Domain.Validation.UserValidation
{
    public class UserValidation
    {
        private readonly IUserRepository _userRepository;

        public UserValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void ValueNotFound(long id)
        {
            if (_userRepository.GetById(id) == null)
            {
                throw new UserException("There is no user with this id");
            }
        }
        public void LoginСorrectness(string login)
        {
            if (login == null)
            {
                throw new UserException("The login cannot be empty/cannot be null");
            }

            if (_userRepository.GetAll().Result.Where(u => u.Login == login).FirstOrDefault() != null)
            {
                throw new UserException("A user with this login already exists");
            }
        }
        public void UserVerification(string login, string hashPassword)
        {
            User user = _userRepository.GetAll().Result.Where(u => u.Login == login).FirstOrDefault();
            if (user == null)
            {
                throw new UserException("There is no user with this login");
            }
            if (user.PasswordHash != hashPassword)
            {
                throw new UserException("The wrong password was entered for this login");
            }
        }
    }
}
