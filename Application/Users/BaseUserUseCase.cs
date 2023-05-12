using Application.Validation.UserValidation;
using Domain.Repositories;

namespace Application.Users
{
    public class BaseUserUseCase
    {
        protected readonly IUserRepository _userRepository;
        protected readonly UserValidation _validationUser;

        public BaseUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _validationUser = new UserValidation(_userRepository);
        }
    }
}
