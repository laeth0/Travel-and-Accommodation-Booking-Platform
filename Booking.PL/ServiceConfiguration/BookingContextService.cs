

using Microsoft.Data.SqlClient;

namespace Booking.PL.ServiceConfiguration
{
    public static class BookingContextService
    {
        public static IServiceCollection AddBookingContextService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string is not found in appsettings.json file");

            if (connectionString == null)
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
                options.UseLazyLoadingProxies().UseSqlServer(connectionString, 
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));

                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);// sets the default query tracking behavior is NoTracking
            });


            return services;
        }
    }
}
