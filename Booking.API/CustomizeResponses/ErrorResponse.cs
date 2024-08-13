




namespace Booking.PL.CustomizeResponses;
public class ErrorResponse : ProblemDetails
{

    public bool IsSuccess { get; set; } = false;
    public string Error { get; set; }

    // type title status detail instance   

}
