




namespace Booking.PL.CustomizeResponses;
public class ErrorResponse : ApiResponse
{
    public IEnumerable<string> Errors { get; set; }

    public ErrorResponse()
    {
        IsSuccess = false;
    }

}
