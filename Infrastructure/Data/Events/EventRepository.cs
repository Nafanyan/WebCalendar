using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Events
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {

        public EventRepository(WebCalendarDbContext dbContext): base(dbContext)
        {
        }

        public async Task<bool> ContainsAsync(long userId, DateTime startEvent, DateTime endEvent)
        {
            return await GetEventAsync(userId, startEvent, endEvent) != null;
        }
        public async Task<Event> GetEventAsync(long userId, DateTime startEvent, DateTime endEvent)
        {
            IReadOnlyList<Event> events = await Entities.Where(e => e.UserId == userId).ToListAsync();
            if (events.Count > 0)
            {
                return await Entities.Where(e => e.UserId == userId)
                .Where(e => (e.StartEvent >= startEvent) &&
                 (e.EndEvent <= endEvent)).FirstOrDefaultAsync();
            }

            return null;
        }
    }
}
