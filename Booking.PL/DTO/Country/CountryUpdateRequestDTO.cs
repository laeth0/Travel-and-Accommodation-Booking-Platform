namespace Booking.PL.DTO.Country
{
    public class CountryUpdateRequestDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } 
        [Required]
        public string Description { get; set; } 
        [Required]
        public IFormFile Image { get; set; } 
    }
}
