using Microsoft.EntityFrameworkCore;

namespace NexallApp
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Defining DbSet properties for each table:
        public DbSet<CarData> CarData { get; set; }
    }
}
