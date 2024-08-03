using Booking.BLL.Interfaces;
using Booking.BLL.IService;
using Booking.BLL.Repositories;
using Booking.DAL.Data;
using Booking.DAL.Entities;
using Booking.PL.MappingProfiles;
using Ecommerce.Presentation.MappingProfiles;
using Ecommerce.Presentation.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace Booking.PL.ServiceConfiguration;
public static class DependencyInjectionService // Extension methods must be created in a non-generic static class
{
    public static IServiceCollection AddDependencyInjectionService(this IServiceCollection services, IConfiguration config)
    {

        var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string is not found in appsettings.json file");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));

            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);// sets the default query tracking behavior is NoTracking
        });


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();

        services.AddAutoMapper(typeof(ApplicationUserProfile), typeof(CityProfile));

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
