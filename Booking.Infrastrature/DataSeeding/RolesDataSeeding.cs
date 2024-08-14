


using Booking.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Booking.Infrastrature.DataSeeding;
public static class RolesDataSeeding
{
    public static async Task SeedRoles(this IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


        if (roleManager.Roles.Any())
            return;


        var type = typeof(ApplicationRoles);
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

        var roles = fields.Select(x => x.GetValue(null).ToString());


        foreach (var role in roles)
            await roleManager.CreateAsync(new IdentityRole(role!));

        return;

    }
}
