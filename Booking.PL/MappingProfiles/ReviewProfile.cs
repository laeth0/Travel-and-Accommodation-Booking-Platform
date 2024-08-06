
namespace Booking.PL.MappingProfiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {

            CreateMap<Review, ReviewResponseDTO>();

            CreateMap<ReviewCreateRequestDTO, Review>();

            CreateMap<ReviewUpdateRequestDTO, Review>();
        }
    }
}
