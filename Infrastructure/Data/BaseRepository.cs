using Domain.Repositories.BasicRepositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public abstract class BaseRepository<TEntity> : IAddedRepository<TEntity>,
        IRemovableRepository<TEntity> where TEntity : class
    {
        protected readonly WebCalendarDbContext DBContext;
        protected DbSet<TEntity> Entities { get; }

        public BaseRepository(WebCalendarDbContext dbContext)
        {
            DBContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);
        }
        public async Task DeleteAsync(TEntity entity)
        {
            Entities.Remove(entity);
        }
    }
}
