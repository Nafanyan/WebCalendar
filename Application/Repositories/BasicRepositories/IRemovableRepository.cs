namespace Application.Repositories.BasicRepositories
{
    public interface IRemovableRepository<TEntity> where TEntity : class
    {
        void Delete( TEntity entety );
    }
}
