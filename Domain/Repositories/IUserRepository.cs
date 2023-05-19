using Domain.Entities;
using Domain.Repositories.BasicRepositories;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IUserRepository : IAddedRepository<User>, IRemovableRepository<User>
    {
        Task<User> GetByIdAsync(long id);
        Task<bool> ContainsAsync(Expression<Func<User, bool>> predicate); 
        Task<IReadOnlyList<Event>> GetEventsAsync(long id);
    }
}
