using Domain.Entities;
using Domain.Repositories.BasicRepositories;

namespace Domain.Repositories
{
    public interface IUserRepository : IAddedRepository<User>, IRemovableRepository<User>
    {
        Task<User> GetById(long id);
        Task<bool> Contains(long id);
        Task<IReadOnlyList<User>> GetAll();
        Task<IReadOnlyList<Event>> GetEvents(long id);
    }
}
