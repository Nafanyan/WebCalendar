using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Events
{
    public class EventRepository : IEventRepository
    {
        private readonly WebCalendareDbContext _dbContext;
        private DbSet<Event> _dbSet => _dbContext.Set<Event>();

        public EventRepository(WebCalendareDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Event entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<bool> Contains(long userId, EventPeriod eventPeriod)
        {
            Event foundEvent = await _dbSet.Where(e => (e.UserId == userId) &&
                 (e.EventPeriod.StartEvent == eventPeriod.StartEvent) &&
                 (e.EventPeriod.EndEvent == eventPeriod.EndEvent)).FirstOrDefaultAsync();
            return foundEvent != null;
        }

        public async Task Delete(Event entity)
        {
            Event foundEvent = await GetEvent(entity.UserId, entity.EventPeriod);
            _dbSet.Remove(foundEvent);
        }

        public async Task<Event> GetEvent(long userId, EventPeriod eventPeriod)
        {

            return await _dbSet.Where(e => (e.UserId == userId) && 
                 (e.EventPeriod.StartEvent == eventPeriod.StartEvent) &&
                 (e.EventPeriod.EndEvent == eventPeriod.EndEvent)).FirstOrDefaultAsync();
        }
    }
}
