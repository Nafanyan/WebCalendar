using Domain.Entitys;
using Domain.Repositories.BasicRepositories;

namespace Domain.Repositories
{
    public interface IUserRepository : IAddedRepository<User>,
        IRemovableRepository<User>
    {
        Task<User> GetUser(long id);
        Task<IReadOnlyList<Event>> GetEvents(User user);
    }
}
