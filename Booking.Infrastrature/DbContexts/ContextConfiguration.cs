


using Booking.Infrastrature.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastrature.DbContexts;
public static class ContextConfiguration
{

    internal static IServiceCollection AddBookingContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (connectionString is null)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "."; 
            builder.InitialCatalog = "Booking"; 
            builder.Add("Trusted_Connection", "True"); 
            builder.MultipleActiveResultSets = true; 
            builder.TrustServerCertificate = true; 

            connectionString = builder.ConnectionString;
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseLazyLoadingProxies()
                    //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // sets the default query tracking behavior is NoTracking
                    .UseSqlServer(connectionString, optionsBuilder =>
                    {
                        optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        //optionsBuilder.EnableRetryOnFailure(5);
                    });
        });



        return services;
    }
}
