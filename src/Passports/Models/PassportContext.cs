using Microsoft.EntityFrameworkCore;

namespace Passports.Models {
    public class PassportContext : DbContext {
        public DbSet<Passport> Passports { get; set; }
        public PassportContext(DbContextOptions<PassportContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Passport>()
                .HasKey(passport => new { passport.Series,passport.Number });
        }
    }
}
