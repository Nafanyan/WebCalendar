namespace Domain.Repositories.BasicRepositories
{
    public interface IUpdatedRepository<TEntity> where TEntity : class
    {
        Task Update(TEntity entity);
    }
}
