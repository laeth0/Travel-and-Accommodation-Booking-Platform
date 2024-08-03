

namespace Booking.PL.ServiceConfiguration;
public static class CustomizingForInvalidResponseService // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddCustomizingForInvalidResponseService(this IServiceCollection services)
    {

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = EndPointcontext =>
            {
                var errors = EndPointcontext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(p => p.ErrorMessage)
                    .ToArray();

                var errorResponse = new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = errors
                };

                var result = new BadRequestObjectResult(errorResponse)
                {
                    ContentTypes = { "application/json" } // Ensure the content type is set to JSON
                };

                return result;
            };
        });


        return services;
    }
}
