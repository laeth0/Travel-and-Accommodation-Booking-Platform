



using Booking.Domain.Entities;
using Booking.Infrastrature.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastrature.DataSeeding;
public static class RoomTypeDataSeeding
{
    public static async Task SeedRoomType(this IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var DbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (await DbContext.Set<RoomType>().AnyAsync())
            return;

        var dataSeed = SeedingRoomTypes();

        await DbContext.Set<RoomType>().AddRangeAsync(dataSeed);
        await DbContext.SaveChangesAsync();
    }

    private static RoomType[] SeedingRoomTypes()
    {
        return [
            new RoomType
            {
                Name = "Standard Room",
                Description = "A comfortable room with a queen bed, ideal for single travelers or couples."
            },
            new RoomType
            {
                Name = "Deluxe Room",
                Description = "Spacious room with a king-size bed and a seating area, perfect for a luxurious stay."
            },
            new RoomType
            {
                Name = "Suite",
                Description = "A large suite with separate living and sleeping areas, providing extra comfort and privacy."
            },
            new RoomType
            {
                Name = "Family Room",
                Description = "A room with two queen beds or one king bed and a pull-out sofa, suitable for families."
            },
            new RoomType
            {
                Name = "Presidential Suite",
                Description = "A luxury suite with multiple rooms, a dining area, and a private balcony with panoramic views."
            },
            new RoomType
            {
                Name = "Penthouse Suite",
                Description = "The most exclusive suite featuring luxurious amenities, private elevator access, and a rooftop terrace."
            },
            new RoomType
            {
                Name = "Executive Room",
                Description = "A room designed for business travelers, with a dedicated workspace and high-speed internet."
            },
            new RoomType
            {
                Name = "Accessible Room",
                Description = "A room designed for guests with disabilities, featuring an accessible bathroom and wider doorways."
            }];

    }

}
