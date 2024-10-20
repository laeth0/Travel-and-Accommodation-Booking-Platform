using System.Net;

namespace TravelAccommodationBookingPlatform.Presentation.Contracts;
public class ApiResponse
{
    public bool IsSuccess { get; set; } = true;

    public string Message { get; set; } 

    public object Data { get; set; }

    public HttpStatusCode StatusCode { get; set; } 


}
