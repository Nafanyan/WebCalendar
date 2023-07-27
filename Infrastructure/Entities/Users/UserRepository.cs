using Application.Entities;
using Application.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Entities.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private DbSet<Event> _dbSetEvent => DBContext.Set<Event>();

        public UserRepository( WebCalendarDbContext dbContext ) : base( dbContext )
        {
        }

        public async Task<bool> ContainsAsync( Expression<Func<User, bool>> predicate )
        {
            return await Entities.Where( predicate ).FirstOrDefaultAsync() != null;
        }
        public async Task<User> GetByIdAsync( long id )
        {
            return await Entities.Where( u => u.Id == id ).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<Event>> GetEventsAsync( long id, DateTime startPeriod, DateTime endPeriod )
        {
            return await _dbSetEvent.Where( e => e.UserId == id )
                .Where( e => ( DateTime.Compare( e.StartEvent, startPeriod ) >= 0 && DateTime.Compare( e.StartEvent, endPeriod ) <= 0 )
                 || ( DateTime.Compare( e.EndEvent, startPeriod ) >= 0 && DateTime.Compare( e.EndEvent, endPeriod ) <= 0 ) )
                .ToListAsync();
        }

        public async Task<User> GetByLoginAsync( string login )
        {
            return await Entities.Where( u => u.Login == login ).FirstOrDefaultAsync();
        }
    }
}
