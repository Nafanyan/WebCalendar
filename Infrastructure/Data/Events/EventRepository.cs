using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Events
{
    public class EventRepository : IEventRepository
    {
        private readonly WebCalendareDbContext _dbContext;
        private DbSet<Event> _dbSetEvent => _dbContext.Set<Event>();
        private DbSet<User> _dbSetUser => _dbContext.Set<User>();

        public EventRepository(WebCalendareDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Event entity)
        {
            await _dbSetEvent.AddAsync(entity);
        }

        public async Task<bool> Contains(long userId, EventPeriod eventPeriod)
        {
            Event foundEvent = await GetEvent(userId, eventPeriod);
            return foundEvent != null;
        }

        public async Task Delete(Event entity)
        {
            Event foundEvent = await GetEvent(entity.UserId, entity.EventPeriod);
            _dbSetEvent.Remove(foundEvent);
        }

        public async Task<Event> GetEvent(long userId, EventPeriod eventPeriod)
        {
            return await _dbSetEvent.Where(e => e.UserId == userId)
                .Where(e => (e.EventPeriod.StartEvent >= eventPeriod.StartEvent) &&
                 (e.EventPeriod.EndEvent <= eventPeriod.EndEvent)).FirstOrDefaultAsync();
        }
    }
}
