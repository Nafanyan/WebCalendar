using Domain.Entities;
using Domain.Repositories;

namespace Application.Users.Queries.GetUsers
{
    public interface IGetUsersQueryHandler
    {
        IReadOnlyList<User> GetUser();
    }

    public class GetUsersQueryHandler : BaseUserUseCase, IGetUsersQueryHandler
    {
        public GetUsersQueryHandler(IUserRepository userRepository) : base(userRepository)
        {
        }

        public IReadOnlyList<User> GetUser()
        {
            return _userRepository.GetAll().Result;
        }
    }
}
