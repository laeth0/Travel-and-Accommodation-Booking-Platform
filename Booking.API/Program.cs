


using Booking.API.MethodTimer;
using Booking.API.Middlewares;
using Booking.Application;
using Booking.Infrastrature;
using Booking.Infrastrature.DataSeeding;

namespace Booking.API;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //i create object from WebApplicationBuilder to determine the configuration of the application
        //dependency injection  هو عبارة عن وعاء بضع فيه الخدمات يلي رح تكون متاحة في المشروع تاعي عشان استعملها فيما بعد عن طريق  Services object  ال 


        builder.Logging.AddDefaultLoggingService();

        builder.Services.AddWebComponents();

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddApplication();



        var app = builder.Build();

        MethodTimeLogger.logger = app.Logger;

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseCors();

        app.UseAuthentication();

        app.UseAuthorization();

        app.Migrate();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "Booking Platform";
            // Swagger وبحولو للواجهة المعروفة تاعت Json file رح يوخذ Swagger بتاع UI وقتها app.UseSwaggerUI  ولما انا بنادي على json file رح يطلع Swagger في النهاية 
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Platform");

        });

        await app.SeedRoles();
        await app.SeedUsers();
        await app.SeedAmenity();
        await app.SeedRoomType();
        await app.SeedResidenceType();

        app.MapControllers();

        app.MapGet("/", () => Results.Content("<h1 style='text-align: center;'>Hello, World!</h1>", "text/html"));

        app.Run();
    }
}
