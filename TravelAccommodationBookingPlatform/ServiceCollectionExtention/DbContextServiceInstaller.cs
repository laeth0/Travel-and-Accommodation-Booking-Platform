
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TravelAccommodationBookingPlatform.Api.Options;
using TravelAccommodationBookingPlatform.Persistence;
using TravelAccommodationBookingPlatform.Persistence.Interceptor;

namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public class DbContextServiceInstaller : IServiceInstaller
{
    public IServiceCollection Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<DatabaseOptionsSetup>();

        services.AddDbContext<AppDbContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

            dbContextOptionsBuilder.UseLazyLoadingProxies();
            dbContextOptionsBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerDbContextOptionsBuilder =>
            {
                sqlServerDbContextOptionsBuilder.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                sqlServerDbContextOptionsBuilder.CommandTimeout(databaseOptions.CommandTimeout);
            });
            dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
            dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            dbContextOptionsBuilder.AddInterceptors(new UpdateAuditableEntitiesInterceptor());
            dbContextOptionsBuilder.UseExceptionProcessor();
        });

        return services;
    }
}
