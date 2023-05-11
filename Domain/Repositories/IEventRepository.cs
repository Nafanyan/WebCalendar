using Domain.Entitys;
using Domain.Repositories.BasicRepositories;

namespace Domain.Repositories
{
    public class KeysEvent
    {
        public long KeyId { get; set; }
    }
    public interface IEventRepository : IRecievedRepository<KeysEvent, Event>, 
        IAddedRepository<Event>, 
        IRemovableRepository<long>,
        IUpdatedRepository<Event>
    {
    }
}
