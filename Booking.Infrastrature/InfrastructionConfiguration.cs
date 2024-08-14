


using Booking.Core.Interfaces.Persistence;
using Booking.Domain.Entities;
using Booking.Infrastrature.Data;
using Booking.Infrastrature.DbContexts;
using Booking.Infrastrature.Persistence;
using Booking.Infrastrature.Services;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastrature;
public static class InfrastructionConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    // this will not work untill  write this line : <FrameworkReference Include="Microsoft.AspNetCore.App" /> in the .csproj file
    {
        services.AddBookingContext(configuration);

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddServices();

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();



        services.AddValidatorsFromAssembly(typeof(InfrastructionConfiguration).Assembly, ServiceLifetime.Transient);
        // the lifetime of the validators is scoped by default


        return services;
    }


    public static IApplicationBuilder Migrate(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();

        return app;
    }


}
