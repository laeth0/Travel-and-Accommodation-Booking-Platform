


namespace Booking.PL.ServiceConfiguration;
public static class DependencyInjectionService // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddDependencyInjectionService(this IServiceCollection services, IConfiguration config)
    {

        var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string is not found in appsettings.json file");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseLazyLoadingProxies().UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));

            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);// sets the default query tracking behavior is NoTracking
        });


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IServiceManager, ServiceManager>();

        services.AddAutoMapper(typeof(ApplicationUserProfile), typeof(CityProfile), typeof(ResidenceProfile));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
