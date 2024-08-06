


namespace Booking.PL.DTO.Review
{
    public class ReviewResponseDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]

        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [Required]

        public Guid RoomBookingId { get; set; }
        [Required]
        public string UserId { get; set; } 

    }
}
