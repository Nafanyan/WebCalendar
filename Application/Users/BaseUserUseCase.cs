using Domain.Repositories;

namespace Application.Users
{
    public class BaseUserUseCase
    {
        protected readonly IUserRepository _userRepository;

        public BaseUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
