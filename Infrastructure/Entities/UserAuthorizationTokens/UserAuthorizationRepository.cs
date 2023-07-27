using Application.Entities;
using Application.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Entities.UserAuthorizationTokens
{
    public class UserAuthorizationRepository : BaseRepository<UserAuthorizationToken>, IUserAuthorizationTokenRepository
    {
        public UserAuthorizationRepository( WebCalendarDbContext dbContext ) : base( dbContext )
        {
        }

        public async Task<bool> ContainsAsync( Expression<Func<UserAuthorizationToken, bool>> predicate )
        {
            return await Entities.Where( predicate ).FirstOrDefaultAsync() != null;
        }

        public async Task<UserAuthorizationToken> GetByRefreshTokenAsync( string refreshToken )
        {
            return await Entities.Where( ua => ua.RefreshToken == refreshToken ).FirstOrDefaultAsync();
        }

        public async Task<UserAuthorizationToken> GetByUserIdAsync( long userId )
        {
            return await Entities.Where( ua => ua.UserId == userId ).FirstOrDefaultAsync();
        }
    }
}
