using Microsoft.EntityFrameworkCore;

namespace NexallApp
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Define your DbSet properties for each entity/table here
        public DbSet<CarData> CarData { get; set; }
    }
}
