

namespace Booking.Domain.Models;

public record PaginationMetadata(int TotalItemCount, int PageSize)
{
    public int TotalPageCount => (int)Math.Ceiling((double)TotalItemCount / PageSize);
}