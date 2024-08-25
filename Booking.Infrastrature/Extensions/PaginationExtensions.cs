using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastrature.Extensions;

public static class PaginationExtensions
{
    public static IQueryable<TItem> GetPage<TItem>(this IQueryable<TItem> queryable, int pageNumber, int pageSize)
    {
        return queryable.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
    }

    public static async Task<PaginationMetadata> GetPaginationMetadataAsync<TItem>(
        this IQueryable<TItem> queryable,
        int pageSize,
        CancellationToken cancellationToken = default
        )
    {
        return new PaginationMetadata(await queryable.CountAsync(cancellationToken), pageSize);
    }
}

