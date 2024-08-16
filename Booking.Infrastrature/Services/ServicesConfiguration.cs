

using Booking.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Booking.Infrastrature.Services;
public static class ServicesConfiguration
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    // this will not work untill  write this line : <FrameworkReference Include="Microsoft.AspNetCore.App" /> in the .csproj file
    {

        services.AddEmailService()
            .AddJwtAuthServices()
            .AddCloudinaryService()
            .AddTransient<IFileService, FileService>();

        return services;
    }
}
