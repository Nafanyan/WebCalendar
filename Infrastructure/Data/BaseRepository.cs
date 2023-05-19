using Domain.Repositories.BasicRepositories;
using Infrastructure.Foundation;

namespace Infrastructure.Data
{
    public abstract class BaseRepository<TEntity> : IAddedRepository<TEntity>,
        IRemovableRepository<TEntity> where TEntity : class
    {
        protected readonly WebCalendareDbContext DBContext;

        public BaseRepository(WebCalendareDbContext dbContext)
        {
            DBContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await DBContext.Set<TEntity>().AddAsync(entity);
        }
        public async Task DeleteAsync(TEntity entity)
        {
            DBContext.Set<TEntity>().Remove(entity);
        }
    }
}
