using Domain.Entities;
using Domain.Repositories.BasicRepositories;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IUserAuthorizationTokenRepository
    {
        void Add(UserAuthorizationToken token);
        Task<UserAuthorizationToken> GetTokenByUserIdAsync(long userId);
        Task<bool> ContainsAsync(Expression<Func<UserAuthorizationToken, bool>> predicate);
    }
}
