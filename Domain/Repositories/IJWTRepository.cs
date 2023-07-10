using Domain.Entities;
using Domain.Repositories.BasicRepositories;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IJWTRepository : IAddedRepository<JWT>
    {
        Task<JWT> GetJWTAsync(long userId);
        Task<bool> ContainsAsync(Expression<Func<JWT, bool>> predicate);
    }
}
