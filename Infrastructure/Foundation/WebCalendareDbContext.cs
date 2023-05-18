using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation
{
    public class WebCalendareDbContext : DbContext
    {
        DbSet<Event> events { get; set; } = null!;
        DbSet<User> users { get; set; } = null!;
        public WebCalendareDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
