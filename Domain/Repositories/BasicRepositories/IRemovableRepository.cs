namespace Domain.Repositories.BasicRepositories
{
    public interface IRemovableRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entety);
    }
}
