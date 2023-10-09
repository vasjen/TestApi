using Microsoft.EntityFrameworkCore;

namespace TestApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.Employee> Employees { get; set; } = null!;
        public DbSet<Models.Work> Works { get; set; } = null!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}