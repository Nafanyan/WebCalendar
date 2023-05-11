using Domain.Entitys;
using Domain.Repositories.BasicRepositories;

namespace Domain.Repositories
{
    public class KeysUser
    {
        public long KeyId { get; set; }
    }
    public interface IUserRepository : IAddedRepository<User>,
        IRemovableRepository<KeysUser>,
        IUpdatedRepository<User>
    {
    }
}
