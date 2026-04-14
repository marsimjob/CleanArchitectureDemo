using Domain.Layer.Models;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Layer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Toy> Toys {  get; set; }
    }
}
