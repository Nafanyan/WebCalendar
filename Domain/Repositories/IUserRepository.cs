using Domain.Entities;
using Domain.Repositories.BasicRepositories;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IUserRepository : 
        IAddedRepository<User>, 
        IRemovableRepository<User>,
        ISearchRepository<User>
    {
        Task<User> GetByIdAsync(long id);
        Task<User> GetByLoginAsync(string login);
        Task<IReadOnlyList<Event>> GetEventsAsync(long id, DateTime startPeriod, DateTime endPeriod);
    }
}
