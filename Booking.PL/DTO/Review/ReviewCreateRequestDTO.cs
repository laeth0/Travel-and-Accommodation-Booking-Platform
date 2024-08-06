namespace Booking.PL.DTO.Review
{
    public class ReviewCreateRequestDTO
    {
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; } 
        [Required]
        public Guid RoomBookingId { get; set; }

    }
}
