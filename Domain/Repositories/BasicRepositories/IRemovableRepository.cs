namespace Domain.Repositories.BasicRepositories
{
    public interface IRemovableRepository<TEntity> where TEntity : class
    {
        Task DeleteAsync(TEntity entety);
    }
}
