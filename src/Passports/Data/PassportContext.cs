using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Passports.Models;
using Z.BulkOperations;

namespace Passports.Data;

public class PassportContext : DbContext {
    public DbSet<Passport> Passports { get; set; }
    public DbSet<AuditPassportEntry> AuditPassportEntries { get; set; }

    public PassportContext(DbContextOptions<PassportContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Passport>()
            .HasKey(passport => new { passport.Series, passport.Number });
        modelBuilder.Entity<Passport>()
            .HasIndex(passport => passport.IsActual);
        // modelBuilder.Entity<Passport>()
        //     .Property<int>("Id").UseIdentityAlwaysColumn();
        modelBuilder.Entity<AuditPassportEntry>()
            .Property<int>("Id");
        // modelBuilder.Entity<Passport>()
        //     .HasKey("Id");
        modelBuilder.Entity<AuditPassportEntry>()
            .HasKey("Id");
        modelBuilder.Entity<Passport>()
            .HasKey(passport => new { passport.Series, passport.Number });
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default) {
        var auditEntries = OnBeforeSaveChanges();
    
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    
        await OnAfterSaveChangesAsync(auditEntries);
        return result;
    }
    
    private List<AuditPassportEntry> OnBeforeSaveChanges() {
        try {
            ChangeTracker.DetectChanges();
        } catch (Exception e) {
            return new List<AuditPassportEntry>();
        }
        var entries = new List<AuditPassportEntry>();
    
        foreach (var entry in ChangeTracker.Entries()) {
            if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged|| !(entry.Entity is Passport))
                continue;
            if (entry.State == EntityState.Modified)
            {
                entry.State = EntityState.Detached;
            }
            var entity = (Passport)entry.Entity;
            var auditEntry = new AuditPassportEntry() {
                IsActual = entity.IsActual,
                ChangeDate = entity.ChangeDate,
                Series = entity.Series, 
                Number = entity.Number
            };
    
            entries.Add(auditEntry);
        }
    
        return entries;
    }
    
    private Task OnAfterSaveChangesAsync(List<AuditPassportEntry> auditEntries) {
        if (auditEntries == null || auditEntries.Count == 0)
            return Task.CompletedTask;
    
        AuditPassportEntries.AddRange(auditEntries);
        return SaveChangesAsync();
    }
}