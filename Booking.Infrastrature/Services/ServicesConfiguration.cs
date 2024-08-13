

using Booking.Domain.Interfaces.Services;
using Booking.Infrastrature.Services.Email;
using Booking.Infrastrature.Services.Files;
using Microsoft.Extensions.DependencyInjection;
using TABP.Infrastructure.Auth.Jwt;


namespace Booking.Infrastrature.Services;
public static class ServicesConfiguration
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    // this will not work untill  write this line : <FrameworkReference Include="Microsoft.AspNetCore.App" /> in the .csproj file
    {

        services.AddEmailService()
            .AddJwtAuthServices()
            .AddTransient<IFileService, FileService>();

        return services;
    }
}
