using Microsoft.AspNetCore.Mvc;

namespace TravelAccommodationBookingPlatform.Presentation.Attributes;
/// <summary>
/// Use this attribute when you want to document an Error Status Code.
/// It assumes that the object-result schema is <see cref="ProblemDetails"/>-like object.
/// </summary>
public class ProducesErrorAttribute : ProducesResponseTypeAttribute
{
    public ProducesErrorAttribute(int errorStatusCode)
        : base(typeof(ProblemDetails), errorStatusCode)
    {
        if (errorStatusCode < 400)
        {
            throw new InvalidOperationException($"{errorStatusCode} is not an error status code");
        }
    }
}
