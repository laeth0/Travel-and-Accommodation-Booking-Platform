using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Domain.Constants;

namespace TravelAccommodationBookingPlatform.Persistence.DataSeed;
public static class RolesDataSeeding
{
    public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (await roleManager.Roles.AnyAsync())
            return;

        var type = typeof(UserRoles);
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

        var roles = fields.Select(x => x.GetValue(null)?.ToString());

        foreach (var role in roles)
            await roleManager.CreateAsync(new IdentityRole(role!));
    }
}
