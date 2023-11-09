using Microsoft.EntityFrameworkCore;

namespace NexallApp
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CarData> CarData { get; set; }
    }
}
