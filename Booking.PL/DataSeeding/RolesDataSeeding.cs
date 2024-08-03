using Booking.BLL.Enums;
using Microsoft.AspNetCore.Identity;


namespace Booking.PL.DataSeeding;
public static class RolesDataSeeding
{
    public static async Task SeedRoles(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = Enum.GetNames(typeof(UserRoles));

        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

    }
}
