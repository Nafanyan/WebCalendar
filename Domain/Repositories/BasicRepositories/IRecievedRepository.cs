
namespace Domain.Repositories.BasicRepositories
{
    public interface IRecievedRepository<DbKey, TEntity> 
        where DbKey : class, IComparable
        where TEntity : class
    {
        TEntity GetByKey(DbKey dbKey);
        IReadOnlyList<TEntity> GetAll();
    }
}
