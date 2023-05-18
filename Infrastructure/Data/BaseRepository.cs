using Domain.Repositories.BasicRepositories;

namespace Infrastructure.Data
{
    public class BaseRepository : IAddedRepository<TEntity>
    {
        public Task Add(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
