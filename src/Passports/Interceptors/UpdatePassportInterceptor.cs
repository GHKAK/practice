using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Passports.Models;

namespace Passports.Interceptors;

public class UpdatePassportInterceptor : SaveChangesInterceptor {
    public override ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default) {
        DbContext? dbContext = eventData.Context;
        if (dbContext is null) {
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        dbContext.ChangeTracker.DetectChanges();
        var entries = dbContext.ChangeTracker.Entries();
        foreach (var entry in entries) {
            if (entry.State is EntityState.Added or EntityState.Modified) {
                // entry.Property(a => a.ChangeDate).CurrentValue = entry.;
                // entry.Property(a => a.IsActual).CurrentValue = entry.Entity.IsActual;
            }
        }

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}