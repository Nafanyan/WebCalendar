using Domain.Entities;
using Domain.Repositories.BasicRepositories;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserAuthorizationTokenRepository : IRemovableRepository<UserAuthorizationToken>
    {
        void Add(UserAuthorizationToken token);
        Task<UserAuthorizationToken> GetTokenByUserIdAsync(long userId);
        Task<UserAuthorizationToken> GetTokenByRefreshTokenAsync(string refreshToken);
        Task<bool> ContainsAsync(Expression<Func<UserAuthorizationToken, bool>> predicate);
    }
}
