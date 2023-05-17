using Domain.Entities;
using Domain.Repositories.BasicRepositories;

namespace Domain.Repositories
{
    public interface IEventRepository : IAddedRepository<Event>, IRemovableRepository<Event>
    {
        Task<Event> GetEvent(long userId, EventPeriod eventPeriod);
        Task<bool> Contains (long userId, EventPeriod eventPeriod);
    }
}
