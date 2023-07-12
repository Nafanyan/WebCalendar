using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data.UserAuthorizationTokens
{
    public class UserAuthorizationRepository : BaseRepository<UserAuthorizationToken>, IUserAuthorizationTokenRepository
    {
        public UserAuthorizationRepository(WebCalendarDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(UserAuthorizationToken token)
        {
            Entities.AddAsync(token);
        }

        public async Task<bool> ContainsAsync(Expression<Func<UserAuthorizationToken, bool>> predicate)
        {
            return await Entities.Where(predicate).FirstOrDefaultAsync() != null;
        }

        public async Task<UserAuthorizationToken> GetTokenByUserIdAsync(long userId)
        {
            return await Entities.Where(ua => ua.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
