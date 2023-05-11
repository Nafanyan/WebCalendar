using Domain.Entitys;
using Domain.Repositories.BasicRepositories;

namespace Domain.Repositories
{
    public interface IEventRepository : IAddedRepository<Event>, 
        IRemovableRepository<Event>,
        IUpdatedRepository<Event>
    {
        Task<Event> GetEvent(long keysEvent, User user);
    }
}
