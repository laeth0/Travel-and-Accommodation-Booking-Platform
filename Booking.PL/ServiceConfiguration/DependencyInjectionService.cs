


namespace Booking.PL.ServiceConfiguration;
public static class DependencyInjectionService // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddDependencyInjectionService(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IServiceManager, ServiceManager>();

        services.AddAutoMapper(typeof(ApplicationUserProfile),
            typeof(CityProfile),
            typeof(ResidenceProfile),
            typeof(CountryProfile),
            typeof(RoomProfile),
            typeof(RoomBookingProfile),
            typeof(ReviewProfile)
            );

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
