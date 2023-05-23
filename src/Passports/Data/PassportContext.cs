using Microsoft.EntityFrameworkCore;
using Passports.Models;

namespace Passports.Data {
    public class PassportContext : DbContext {
        public DbSet<Passport> Passports { get; set; }


        public PassportContext(DbContextOptions<PassportContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Passport>()
                .HasKey(passport => new { passport.Series,passport.Number });
            modelBuilder.Entity<Passport>()
                .HasIndex(passport => passport.IsActual);
        }
    }
}
