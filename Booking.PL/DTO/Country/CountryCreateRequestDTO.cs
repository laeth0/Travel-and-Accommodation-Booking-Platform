namespace Booking.PL.DTO.Country
{
    public class CountryCreateRequestDTO
    {
        [Required]
        public string Name { get; set; } 
        [Required]
        public string Description { get; set; } 
        [Required]
        public IFormFile Image { get; set; } 
    }
}
