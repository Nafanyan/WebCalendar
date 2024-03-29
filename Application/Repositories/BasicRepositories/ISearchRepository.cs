﻿using System.Linq.Expressions;

namespace Application.Repositories.BasicRepositories
{
    public interface ISearchRepository<TEntity> where TEntity : class
    {
        Task<bool> ContainsAsync( Expression<Func<TEntity, bool>> predicate );
    }
}
