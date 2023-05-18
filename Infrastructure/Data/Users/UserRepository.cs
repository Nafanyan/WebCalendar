﻿using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Data.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private DbSet<User> _dbSetUser => DBContext.Set<User>();
        private DbSet<Event> _dbSetEvent => DBContext.Set<Event>();

        public UserRepository(WebCalendareDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> Contains(Expression<Func<User, bool>> predicate)
        {
            return await _dbSetUser.Where(predicate).FirstOrDefaultAsync() != null;
        }
        public async Task<IReadOnlyList<User>> GetAll()
        {
            return await _dbSetUser.ToListAsync();
        }
        public async Task<User> GetById(long id)
        {
            return await _dbSetUser.Where(u => u.Id == id).FirstAsync();
        }
        public async Task<IReadOnlyList<Event>> GetEvents(long id)
        {
            return await _dbSetEvent.Where(e => e.UserId == id).ToListAsync();
        }
    }
}
