namespace Booking.PL.DTO.City
{
    public class CityCreateRequestDTO
    {
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
    }
}
