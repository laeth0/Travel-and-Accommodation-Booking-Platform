using System.Net;


namespace Booking.PL.CustomizeResponses;
public abstract class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
}
