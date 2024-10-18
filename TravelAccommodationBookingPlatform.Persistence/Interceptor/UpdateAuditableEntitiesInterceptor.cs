using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Interceptor;

public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
           DbContextEventData eventData,
           InterceptionResult<int> result,
           CancellationToken cancellationToken = default
           )
    {

        var context = eventData.Context;

        if (context is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);


        var utcNow = DateTime.UtcNow;

        context.ChangeTracker.DetectChanges();

        var entries = context.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entry in entries)
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(a => a.CreatedAt).CurrentValue = utcNow;
                    break;
                case EntityState.Modified:
                    entry.Property(a => a.UpdatedAt).CurrentValue = utcNow;
                    break;
            }

        return new ValueTask<InterceptionResult<int>>(result);
    }
}
