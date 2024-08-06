


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
        builder.Services.AddDependencyInjectionService();
        builder.Services.AddConfigurationForSwaggerService();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddBookingContextService(builder.Configuration);

        builder.Logging.AddDefaultLoggingService();


        builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(3);
        });


        var app = builder.Build();

        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "Booking Platform";
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Platform");            
        });
        app.UseRouting();//  ⇒ This line configures the middleware to enable routing. Routing determines how URLs are mapped to controllers and actions.
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();//⇒ This line configures the middleware to enable authorization. It indicates that the application should process authorization requirements.
        app.UseHttpsRedirection();// ⇒ This line configures the application to automatically redirect HTTP requests to HTTPS. This is a common practice for improving security.
        app.MapControllers();
        await RolesDataSeeding.SeedRoles(app);
        await UsersDataSeeding.SeedUsers(app);
        app.Run();
    }
}