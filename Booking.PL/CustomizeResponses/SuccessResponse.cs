


namespace Booking.PL.CustomizeResponses;
public class SuccessResponse : ApiResponse
{
    public string Message { get; set; }
    public object Result { get; set; }

    public SuccessResponse()
    {
        IsSuccess = true;
        Message ??= DefaultMessage(StatusCode); // (??=) is null-coalescing assignment operator
    }

    private string DefaultMessage(HttpStatusCode statusCode) => statusCode switch
    {
        HttpStatusCode.OK => "Success",
        HttpStatusCode.Created => "Created",
        HttpStatusCode.NoContent => "No Content",
        HttpStatusCode.BadRequest => "Bad Request",
        HttpStatusCode.Unauthorized => "Unauthorized",
        HttpStatusCode.NotFound => "Data Not Found",
        HttpStatusCode.InternalServerError => "Server Error",
        _ => "Error"
    };


}
