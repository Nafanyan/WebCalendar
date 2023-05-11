using Domain.Entitys;
using Domain.Repositories.BasicRepositories;

namespace Domain.Repositories
{
    public interface IUserRepository : IAddedRepository<User>, IRemovableRepository<User>
    {
        Task<User> GetById(long id);
        Task<IReadOnlyList<Event>> GetEvents(long id);
    }
}
