

namespace Booking.PL.DataSeeding;
public static class UsersDataSeeding
{
    public static async Task SeedUsers(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync("Manager"))
            await roleManager.CreateAsync(new IdentityRole("Manager"));


        // Check if the manager user already exists
        if (await userManager.FindByEmailAsync("Manager@gmail.com") is null)
        {
            var user = new ApplicationUser
            {
                FirstName = "Manager",
                LastName = "Manager",
                UserName = "Manager",
                Email = "Manager@gmail.com",
                EmailConfirmed = true,// Assuming email is confirmed for seeding purposes
                PhoneNumber = "059988776655",
                PhoneNumberConfirmed = true,
                ImageName = default
            };

            // Create the user with a password
            var result = await userManager.CreateAsync(user, "Manager@123");

            // If the user was successfully created, add them to the Manager role
            if (result.Succeeded)
                await userManager.AddToRoleAsync(user, "Manager");
            else
                throw new Exception("Failed to create the manager user");  // Handle the case where the user couldn't be created
        }

    }
}