

using Booking.Domain.Entities;
using Booking.Infrastrature.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastrature.DataSeeding;
public static class AmenityDataSeeding
{

    public static async Task SeedAmenity(this IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var DbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (await DbContext.Set<Amenity>().AnyAsync())
            return;

        var dataSeed = SeedingAmenities();

        await DbContext.Set<Amenity>().AddRangeAsync(dataSeed);
        await DbContext.SaveChangesAsync();
    }

    private static Amenity[] SeedingAmenities()
    {
        return [
            new Amenity
            {
                Name = "Free Wi-Fi",
                Description = "High-speed wireless internet access available in all rooms and public areas."
            },
            new Amenity
            {
                Name = "Swimming Pool",
                Description = "Outdoor heated pool open from 6 AM to 10 PM with towel service."
            },
            new Amenity
            {
                Name = "Fitness Center",
                Description = "Fully equipped gym with modern workout machines, free weights, and yoga mats."
            },
            new Amenity
            {
                Name = "Room Service",
                Description = "24/7 room service offering a variety of international and local cuisine."
            },
            new Amenity
            {
                Name = "Parking",
                Description = "Complimentary valet and self-parking available for all hotel guests."
            },
            new Amenity
            {
                Name = "Airport Shuttle",
                Description = "Free shuttle service to and from the airport, running every 30 minutes."
            },
            new Amenity
            {
                Name = "Spa",
                Description = "Full-service spa offering massages, facials, and body treatments."
            },
            new Amenity
            {
                Name = "Business Center",
                Description = "24-hour business center equipped with computers, printers, and meeting rooms."
            },
            new Amenity
            {
                Name = "Concierge Service",
                Description = "Dedicated concierge service to assist with restaurant reservations and local attractions."
            },
            new Amenity
            {
                Name = "Pet Friendly",
                Description = "Pets allowed with no extra charge. Includes pet-friendly rooms and walking areas."
            }];

    }


}
