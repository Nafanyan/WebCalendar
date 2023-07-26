using Application.Entities;
using Application.Repositories.BasicRepositories;

namespace Application.Repositories
{
    public interface IEventRepository : IAddedRepository<Event>, IRemovableRepository<Event>
    {
        Task<Event> GetEventAsync(long userId, DateTime startEvent, DateTime endEvent);
        Task<bool> ContainsAsync(long userId, DateTime startEvent, DateTime endEvent);
    }
}
