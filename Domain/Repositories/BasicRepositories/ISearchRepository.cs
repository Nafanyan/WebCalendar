using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Repositories.BasicRepositories
{
    public interface ISearchRepository<TEntity> where TEntity : class
    {
        Task<bool> ContainsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
