using Infrastructure.Entities.Events;
using Infrastructure.Entities.UserAuthorizationTokens;
using Infrastructure.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation
{
    public class WebCalendarDbContext : DbContext
    {
        public WebCalendarDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserAuthorizationTokenConfiguration());
        }
    }
}
