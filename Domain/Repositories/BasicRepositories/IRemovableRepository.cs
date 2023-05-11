
namespace Domain.Repositories.BasicRepositories
{
    public interface IRemovableRepository<DbKey> where DbKey : class
    {
        void Delete(DbKey key);
    }
}
