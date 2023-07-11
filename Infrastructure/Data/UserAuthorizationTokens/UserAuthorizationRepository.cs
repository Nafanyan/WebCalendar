using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation;
using System.Linq.Expressions;

namespace Infrastructure.Data.UserAuthorizationTokens
{
    public class UserAuthorizationRepository : BaseRepository<UserAuthorizationToken>, IUserAuthorizationRepository
    {
        public UserAuthorizationRepository(WebCalendarDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> ContainsAsync(Expression<Func<UserAuthorizationToken, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<UserAuthorizationToken> GetTokenAsync(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
