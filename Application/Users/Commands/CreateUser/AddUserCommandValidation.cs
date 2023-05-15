using Domain.Repositories;

namespace Application.Users.Commands.CreateUser
{
    class AddUserCommandValidation : IValidation<AddUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Validation(AddUserCommand command)
        {
            if (command.Login == null)
            {
                return "The login cannot be empty/cannot be null";
            }

            if (_userRepository.GetAll().Result.Where(u => u.Login == command.Login).
                FirstOrDefault() != null)
            {
                return "A user with this login already exists";
            }

            return "Ok";
        }
    }
}
