using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Events
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        private DbSet<Event> _dbSetEvent => DBContext.Set<Event>();

        public EventRepository(WebCalendareDbContext dbContext): base(dbContext)
        {
        }

        public async Task<bool> ContainsAsync(long userId, EventPeriod eventPeriod)
        {
            return await GetEventAsync(userId, eventPeriod) != null;
        }
        public async Task<Event> GetEventAsync(long userId, EventPeriod eventPeriod)
        {
            return await _dbSetEvent.Where(e => e.UserId == userId)
                .Where(e => (e.EventPeriod.StartEvent >= eventPeriod.StartEvent) &&
                 (e.EventPeriod.EndEvent <= eventPeriod.EndEvent)).FirstOrDefaultAsync();
        }
    }
}
