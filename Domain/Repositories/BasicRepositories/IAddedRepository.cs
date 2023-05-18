namespace Domain.Repositories.BasicRepositories
{
    public interface IAddedRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
    }
}
