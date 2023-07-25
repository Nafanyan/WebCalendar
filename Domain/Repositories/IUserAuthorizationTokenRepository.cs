using Domain.Entities;
using Domain.Repositories.BasicRepositories;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserAuthorizationTokenRepository : 
        IAddedRepository<UserAuthorizationToken>, 
        IRemovableRepository<UserAuthorizationToken>,
        ISearchRepository<UserAuthorizationToken>
    {
        Task<UserAuthorizationToken> GetByUserIdAsync(long userId);
        Task<UserAuthorizationToken> GetByRefreshTokenAsync(string refreshToken);
    }
}
