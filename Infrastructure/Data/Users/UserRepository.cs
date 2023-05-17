using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly WebCalendareDbContext _dbContext;
        private DbSet<User> _dbSetUser => _dbContext.Set<User>();
        private DbSet<Event> _dbSetEvent => _dbContext.Set<Event>();

        public UserRepository(WebCalendareDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User entity)
        {
            await _dbSetUser.AddAsync(entity);
        }

        public async Task<bool> Contains(Expression<Predicate<User>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(User entity)
        {
            User user = await GetById(entity.Id);
            _dbSetUser.Remove(user);
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
