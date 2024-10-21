using System.Net;

namespace TravelAccommodationBookingPlatform.Presentation.Contracts;
public class ApiResponse
{
    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public object? Data { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    public ApiResponse(object? data, HttpStatusCode statusCode = HttpStatusCode.OK, string? message = null)
    {
        Data = data;
        StatusCode = statusCode;
        Message = message ?? DefaultMessage(statusCode);
        IsSuccess = true;
    }

    private string DefaultMessage(HttpStatusCode statusCode) => statusCode switch
    {
        HttpStatusCode.OK => "Success",
        HttpStatusCode.Created => "Resource created",
        HttpStatusCode.NoContent => "Resource deleted",
        _ => "Error",
    };




}
