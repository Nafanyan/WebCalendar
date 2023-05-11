using Domain.Entitys;
using Domain.Repositories.BasicRepositories;

namespace Domain.Repositories
{
    public interface IEventRepository : IAddedRepository<Event>, IRemovableRepository<Event>
    {
        Task<Event> GetEvent(long id, User user);
    }
}
