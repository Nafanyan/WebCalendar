namespace Application.Repositories.BasicRepositories
{
    public interface IAddedRepository<TEntity> where TEntity : class
    {
        void Add( TEntity entety );
    }
}
