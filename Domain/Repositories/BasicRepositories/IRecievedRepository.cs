
namespace Domain.Repositories.BasicRepositories
{
    public interface IRecievedRepository<DbKey, TEntity> 
        where DbKey : class
        where TEntity : class
    {
        Task<TEntity> GetByKey(DbKey dbKey);
        Task<IReadOnlyList<TEntity>> GetAll();
    }
}
