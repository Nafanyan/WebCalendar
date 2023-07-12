using Domain.Entities;
using Domain.Repositories.BasicRepositories;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IUserAuthorizationTokenRepository : IAddedRepository<UserAuthorizationToken>
    {
        Task<UserAuthorizationToken> GetTokenByUserIdAsync(long userId);
        Task<bool> ContainsAsync(Expression<Func<UserAuthorizationToken, bool>> predicate);
    }
}
