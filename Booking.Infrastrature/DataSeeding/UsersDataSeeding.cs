


using Booking.Domain;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Booking.Infrastrature.DataSeeding;
public static class UsersDataSeeding
{
    public static async Task SeedUsers(this IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();



        if (!await roleManager.Roles.AnyAsync() ||
            await userManager.FindByEmailAsync("Manager@gmail.com") is { })
            return;


        var user = new ApplicationUser
        {
            FirstName = "Manager",
            LastName = "Manager",
            UserName = "Manager",
            Email = "manager@gmail.com",
            EmailConfirmed = true,// Assuming email is confirmed for seeding purposes
            PhoneNumber = "059988776655",
            PhoneNumberConfirmed = true,
        };

        // Create the user with a password
        var result = await userManager.CreateAsync(user, "Manager@123");


        // If the user was successfully created, add them to the Manager role
        if (result.Succeeded)
            await userManager.AddToRoleAsync(user, ApplicationRoles.Manager);
        else
        {
            var errors = result.Errors.Aggregate("", (acc, error) => acc + error.Description + "\n");
            throw new Exception($"Failed to create the manager user\n {errors}");
        }



    }
}