using Domain.UnitOfWork;

namespace Infrastructure.Foundation
{
    public class UnitOdWork : IUnitOfWork
    {
        private readonly WebCalendareDbContext _dbContext;

        public UnitOdWork(WebCalendareDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
