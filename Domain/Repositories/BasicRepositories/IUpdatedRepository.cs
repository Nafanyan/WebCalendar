
namespace Domain.Repositories.BasicRepositories
{
    public interface IUpdatedRepository<TEntity> where TEntity : class
    {
        void Update(TEntity entity);
    }
}
