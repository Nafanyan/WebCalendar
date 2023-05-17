using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation
{
    public class WebCalendareDbContext : DbContext
    {
        public WebCalendareDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
