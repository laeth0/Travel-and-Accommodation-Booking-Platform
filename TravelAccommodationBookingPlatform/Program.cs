using Serilog;
using TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;
using TravelAccommodationBookingPlatform.Api.WebApplicationExtensions;


var builder = WebApplication.CreateBuilder(args);
{

    builder.AddServiceDefaults();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpContextAccessor();
    builder.Services.InstallServices(builder.Configuration, typeof(IServiceInstaller).Assembly);
    builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
}


var app = builder.Build();
{
    app.UseGlobalErrorHandling();

    app.MapDefaultEndpoints();

    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Booking Platform";
        // Swagger ������ ������� �������� ���� Json file �� ���� Swagger ���� UI ����� app.UseSwaggerUI  ���� ��� ����� ��� json file �� ���� Swagger �� ������� 
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Platform");

    });

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

    app.MapGet("/", () => Results.Content("<h1 style='text-align: center;'>Hello, World!</h1>", "text/html"));

    app.Run();
}
