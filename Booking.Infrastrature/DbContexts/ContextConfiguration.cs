


using Booking.Infrastrature.Data;
using Booking.Infrastrature.Interceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastrature.DbContexts;
public static class ContextConfiguration
{

    internal static IServiceCollection AddBookingContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseLazyLoadingProxies();

            options.UseSqlServer(connectionString);

            options.AddInterceptors(new UpdateAuditableEntitiesInterceptor());
        });

        return services;
    }


}
