namespace Booking.PL.DTO.Review
{
    public class ReviewUpdateRequestDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; } 

    }
}
