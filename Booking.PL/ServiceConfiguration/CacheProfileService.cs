


using System.Text.Json.Serialization;

namespace Booking.PL.ServiceConfiguration;
public static class CacheProfileService // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddCacheProfileService(this IServiceCollection services)
    {
        /*
         1.	Controller Support: Registers the necessary services to support controllers
         2.	Routing: Adds routing services that allow you to define URL patterns and map them to specific controller actions.
         3.	Model Binding: Enables model binding, which automatically maps data from HTTP requests (such as query strings, form data, and JSON payloads) to action method parameters.
         4.	Validation: Integrates model validation, ensuring that incoming data meets specified validation rules before it reaches your controller actions.
         5.	Formatters: Adds support for input and output formatters, allowing your API to handle different data formats like JSON and XML.
         
         it call a method AddApiExplorer() that Adds the default API explorer service just for Controllers api.(EndPoint for Controllers api يعني بضفلي سيرفيس انو يبحث جوا المشروع تاعي على )
         */
        services.AddControllers(options =>
        {
            options.CacheProfiles.Add("Default60Sec", new CacheProfile
            {
                Duration = 60,
                Location = ResponseCacheLocation.Client,
            });
        })
            // Configures JsonOptions for the specified builder. Uses default values from JsonSeria1izerDefautts . 
            .AddJsonOptions(options => 
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);//this line to ignore the loop reference in the json response

        return services;
    }
}
