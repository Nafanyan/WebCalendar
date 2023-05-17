using Domain.Entities;
using Domain.Repositories.BasicRepositories;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IUserRepository : IAddedRepository<User>, IRemovableRepository<User>
    {
        Task<User> GetById(long id);
        Task<bool> Contains(Expression<Predicate<User>> predicate);
        Task<IReadOnlyList<User>> GetAll();
        Task<IReadOnlyList<Event>> GetEvents(long id);
    }
}
