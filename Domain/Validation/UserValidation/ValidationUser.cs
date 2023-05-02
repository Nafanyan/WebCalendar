using WebCalendar.Domain.Users;

namespace WebCalendar.Domain.Validation.UserValidation
{
    public class ValidationUser
    {
        private readonly IUserRepository _userRepository;

        public ValidationUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void CheckingContentInRepository(long id)
        {
            if (_userRepository.GetById(id) == null)
            {
                throw new UserException("There is no user with this id");
            }
        }

        public void CheckingLogin(string login)
        {
            if (login == null)
            {
                throw new UserException("The login cannot be empty/cannot be null");
            }

            if (_userRepository.GetAll().Where(u => u.Login == login).FirstOrDefault() != null)
            {
                throw new UserException("A user with this login already exists");
            }
        }

        public void CheckingUser(string login, string hashPassword)
        {
            User user = _userRepository.GetByLogin(login);
            if (user == null)
            {
                throw new UserException("There is no user with this login");
            }
            if (!user.CheckPasswordHash(hashPassword))
            {
                throw new UserException("The wrong password was entered for this login");
            }
        }
    }
}
