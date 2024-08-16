

namespace Booking.Domain.Models;
public class CloudinaryImage
{
    public string PublicId { get; set; }
    public string Url { get; set; }

    // Deconstruct method for destructuring
    public void Deconstruct(out string publicId, out string url)
    {
        publicId = PublicId;
        url = Url;
    }

}
