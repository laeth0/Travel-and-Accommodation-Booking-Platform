using System.Linq.Expressions;
using TravelAccommodationBookingPlatform.Domain.Enums;

namespace TravelAccommodationBookingPlatform.Domain.Models;
public record Query<TEntity>(
  Expression<Func<TEntity, bool>> Filter,
  SortOrder SortOrder,
  string? SortColumn,
  int PageNumber,
  int PageSize);