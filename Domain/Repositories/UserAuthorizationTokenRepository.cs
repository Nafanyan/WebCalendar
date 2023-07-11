using Domain.Entities;
using Domain.Repositories.BasicRepositories;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IUserAuthorizationRepository : IAddedRepository<UserAuthorizationToken>
    {
        Task<UserAuthorizationToken> GetTokenAsync(long userId);
        Task<bool> ContainsAsync(Expression<Func<UserAuthorizationToken, bool>> predicate);
    }
}
