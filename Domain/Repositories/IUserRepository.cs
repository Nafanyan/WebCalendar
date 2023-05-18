using Domain.Entities;
using Domain.Repositories.BasicRepositories;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IUserRepository : IAddedRepository<User>, IRemovableRepository<User>
    {
        Task<User> GetByIdAsync(long id);
        Task<bool> ContainsAsync(Expression<Predicate<User>> predicate); 
        Task<IReadOnlyList<User>> GetAllAsync();
        Task<IReadOnlyList<Event>> GetEventsAsync(long id);
    }
}
