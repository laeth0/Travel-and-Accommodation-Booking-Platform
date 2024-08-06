namespace Booking.PL.DTO.Country
{
    public class CountryResponseDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } 
        [Required]
        public string Description { get; set; } 
        [Required]
        public string ImageName { get; set; } 
    }
}
