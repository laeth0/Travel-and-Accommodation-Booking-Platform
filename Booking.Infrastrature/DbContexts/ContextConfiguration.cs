


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

            builder.DataSource = "."; // the server name
            builder.InitialCatalog = "Booking"; // the database name
            builder.Add("Trusted_Connection", "True"); // use Windows Authentication
            builder.MultipleActiveResultSets = true; // allow multiple queries to be executed in the same command
            builder.TrustServerCertificate = true; // don't verify the server's SSL certificate

            // If using Azure SQL Edge.
            // builder.DataSource = "tcp:127.0.0.1,1433";

            // Because we want to fail fast. Default is 15 seconds.
            //builder.ConnectTimeout = 3;

            // If using Windows Integrated authentication.
            //builder.IntegratedSecurity = true;

            // If using SQL Server authentication.
            // builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USR");
            // builder.Password = Environment.GetEnvironmentVariable("MY_SQL_PWD");

            connectionString = builder.ConnectionString;
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseLazyLoadingProxies()
                    //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // sets the default query tracking behavior is NoTracking
                    .UseSqlServer(connectionString, optionsBuilder =>
                    {
                        optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        //optionsBuilder.EnableRetryOnFailure(5);// read this https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                    });
        });



        return services;
    }
}
