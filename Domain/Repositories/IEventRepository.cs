using Domain.Entities;
using Domain.Repositories.BasicRepositories;

namespace Domain.Repositories
{
    public interface IEventRepository : IAddedRepository<Event>, IRemovableRepository<Event>
    {
        Task<Event> GetEventAsync(long userId, EventPeriod eventPeriod);
        Task<bool> ContainsAsync(long userId, EventPeriod eventPeriod);
    }
}
