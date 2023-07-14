﻿using Domain.Repositories.BasicRepositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public abstract class BaseRepository<TEntity> : IAddedRepository<TEntity>,
        IRemovableRepository<TEntity> where TEntity : class
    {
        protected readonly WebCalendarDbContext DBContext;
        protected DbSet<TEntity> Entities => DBContext.Set<TEntity>();

        public BaseRepository(WebCalendarDbContext dbContext)
        {
            DBContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            Entities.AddAsync(entity);
        }
        public void Delete(TEntity entity)
        {
            Entities.Remove(entity);
        }
    }
}
