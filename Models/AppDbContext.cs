using Microsoft.EntityFrameworkCore;

namespace TI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public DbSet<History> History { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Video> Video { get; set; }
    }
}