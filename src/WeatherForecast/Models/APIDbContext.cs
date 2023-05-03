
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace WeatherForecast.Models {
    public class APIDbContext : DbContext {
        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public APIDbContext(DbContextOptions<APIDbContext> options):base(options) {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            base.OnModelCreating(modelBuilder);
        }
    }
}
