


using System.Net;

namespace Booking.API.CustomizeResponses;
public class SuccessResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public object data { get; set; }



    public SuccessResponse()
    {
        StatusCode = HttpStatusCode.OK;
        Message = "Data Retrieved Successfully";
        IsSuccess = true;
        data = new object();
        //Message ??= DefaultMessage(StatusCode); // (??=) is null-coalescing assignment operator
    }

    //private string DefaultMessage(HttpStatusCode statusCode) => statusCode switch
    //{
    //    HttpStatusCode.OK => "Success",
    //    HttpStatusCode.Created => "Created",
    //    HttpStatusCode.NoContent => "No Content",
    //    HttpStatusCode.BadRequest => "Bad Request",
    //    HttpStatusCode.Unauthorized => "Unauthorized",
    //    HttpStatusCode.NotFound => "Data Not Found",
    //    HttpStatusCode.Conflict => "Conflict",
    //    HttpStatusCode.InternalServerError => "Server Error",
    //    _ => "Error"
    //};


}
