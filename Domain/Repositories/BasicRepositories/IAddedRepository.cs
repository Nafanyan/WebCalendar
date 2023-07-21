namespace Domain.Repositories.BasicRepositories
{
    public interface IAddedRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entety);
    }
}
