using Serilog;
using TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;
using TravelAccommodationBookingPlatform.Api.WebApplicationExtensions;


var builder = WebApplication.CreateBuilder(args);
{

    builder.AddServiceDefaults();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.InstallServices(builder.Configuration, typeof(IServiceInstaller).Assembly);
    builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
}


var app = builder.Build();
{
    app.UseGlobalErrorHandling();

    app.MapDefaultEndpoints();

    app.UseSwagger();

    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseCors();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseRateLimiter();

    app.MapControllers();

    if (app.Environment.IsDevelopment())
    {
        await app.Migrate();
    }

    app.Run();
}
