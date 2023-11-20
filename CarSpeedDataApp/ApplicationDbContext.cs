using Microsoft.EntityFrameworkCore;

namespace CarSpeedDataApp
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CarData> CarData { get; set; }
    }
}
