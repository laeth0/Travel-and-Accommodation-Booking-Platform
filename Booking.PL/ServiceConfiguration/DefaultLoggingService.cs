namespace Booking.PL.ServiceConfiguration
{
    public static class DefaultLoggingService
    {
        public static ILoggingBuilder AddDefaultLoggingService(this ILoggingBuilder services)
        {
            // watch this vedio https://www.youtube.com/watch?v=JUHJZhbIloM
            // and watch this vedio https://www.youtube.com/watch?v=99YFw2Qb2Ho
            services.ClearProviders(); // Clear all providers => the logging provider is Console, Debug, EventSource, EventLog ( Windows only )
            services.AddConsole(); // Add Console provider
            return services;
        }
    }
}
