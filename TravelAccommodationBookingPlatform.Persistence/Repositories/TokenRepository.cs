using TravelAccommodationBookingPlatform.Application.Interfaces.Persistence.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class TokenRepository : Repository<Token>, ITokenRepository
{
    public TokenRepository(AppDbContext context) : base(context)
    {
    }


}
