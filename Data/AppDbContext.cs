using Microsoft.EntityFrameworkCore;
using UserDetails.Model;

namespace UserDetails.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
