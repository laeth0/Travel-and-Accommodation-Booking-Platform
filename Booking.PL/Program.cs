


using Booking.PL.CustomizeResponses;
using Booking.PL.DataSeeding;
using Booking.PL.ServiceConfiguration;
using Ecommerce.Presentation.DataSeeding;
using Microsoft.AspNetCore.Identity;

namespace Booking.PL;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCacheProfileService();
        builder.Services.AddConfigCORSService();
        builder.Services.AddConfigService(builder.Configuration);
        builder.Services.AddConfigureApiForJWTService(builder.Configuration);
        builder.Services.AddCustomizingForInvalidResponseService();
        builder.Services.AddDependencyInjectionService(builder.Configuration);
        builder.Services.AddJWTConfigurationForSwaggerService();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Logging.AddDefaultLoggingService();


        builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(3);
        });

        var app = builder.Build();


        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors();
        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();//⇒ This line configures the middleware to enable authorization. It indicates that the application should process authorization requirements.
        app.UseRouting();//  ⇒ This line configures the middleware to enable routing. Routing determines how URLs are mapped to controllers and actions.
        app.UseHttpsRedirection();// ⇒ This line configures the application to automatically redirect HTTP requests to HTTPS. This is a common practice for improving security.
        await RolesDataSeeding.SeedRoles(app);
        await UsersDataSeeding.SeedUsers(app);
        app.MapGet("/", () => "Hello World!");
        app.MapGet("Images/{ImageName}", (string ImageName) =>
        {
            var Imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", "images", ImageName);
            if (File.Exists(Imagepath))
            {
                //using var FileStream = new FileStream(Imagepath, FileMode.Open);
                var fileStream = new FileStream(Imagepath, FileMode.Open, FileAccess.Read, FileShare.Read);

                return Results.File(fileStream, "image/png");
            }
            return Results.NotFound(new ErrorResponse
            {
                Errors = new List<string> { "Image Not Found" }
            });
        });
        app.Run();
    }
}