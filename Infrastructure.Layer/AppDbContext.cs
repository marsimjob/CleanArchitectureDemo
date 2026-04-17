using Domain.Layer.Models;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Layer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Toy> Toys {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add the hasquery filter os EF automatiicallky excludes IsDeleted
            modelBuilder.Entity<Toy>().HasQueryFilter(t => !t.IsDeleted);
        }
    }
}
