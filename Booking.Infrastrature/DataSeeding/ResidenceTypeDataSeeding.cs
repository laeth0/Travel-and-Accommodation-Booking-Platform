using Booking.Domain.Entities;
using Booking.Infrastrature.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastrature.DataSeeding;
public static class ResidenceTypeDataSeeding
{

    public static async Task SeedResidenceType(this IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var DbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (await DbContext.Set<ResidenceType>().AnyAsync())
            return;

        var dataSeed = SeedingResidenceType();

        await DbContext.Set<ResidenceType>().AddRangeAsync(dataSeed);
        await DbContext.SaveChangesAsync();
    }

    private static ResidenceType[] SeedingResidenceType()
    {
        return [
            new ResidenceType
            {
                Name = "Studio Apartment",
                Description = "A compact apartment with a combined living area and bedroom, ideal for solo travelers or couples."
            },
            new ResidenceType
            {
                Name = "One-Bedroom Apartment",
                Description = "An apartment with a separate bedroom and living area, offering more space and comfort."
            },
            new ResidenceType
            {
                Name = "Two-Bedroom Apartment",
                Description = "An apartment with two separate bedrooms, suitable for families or small groups."
            },
            new ResidenceType
            {
                Name = "Loft",
                Description = "A stylish open-plan apartment, often with high ceilings and large windows."
            },
            new ResidenceType
            {
                Name = "Condominium",
                Description = "A privately-owned apartment or suite available for short-term or long-term rental."
            },
            new ResidenceType
            {
                Name = "Villa",
                Description = "A luxurious detached residence, typically with multiple rooms and private outdoor space."
            },
            new ResidenceType
            {
                Name = "Townhouse",
                Description = "A multi-level residence with shared walls, offering more privacy and space than an apartment."
            },
            new ResidenceType
            {
                Name = "Cottage",
                Description = "A small, cozy standalone house, often located in rural or vacation areas."
            },
            new ResidenceType
            {
                Name = "Bungalow",
                Description = "A single-story detached residence, often with a garden, ideal for a peaceful retreat."
            },
            new ResidenceType
            {
                Name = "Penthouse",
                Description = "A luxurious apartment located on the top floor of a building, offering stunning views and upscale amenities."
            },
            new ResidenceType
            {
                Name = "Cabin",
                Description = "A rustic, standalone wooden structure often found in natural surroundings such as forests or mountains."
            },
            new ResidenceType
            {
                Name = "Beach House",
                Description = "A standalone residence located near the beach, typically featuring ocean views and easy access to the shore."
            },
            new ResidenceType
            {
                Name = "Farmhouse",
                Description = "A rural residence on a farm or agricultural property, offering a countryside experience."
            },
            new ResidenceType
            {
                Name = "Guesthouse",
                Description = "A smaller, separate residence located on the grounds of a larger property, often used for visitors."
            },
            new ResidenceType
            {
                Name = "Mansion",
                Description = "A large, stately residence with multiple rooms, luxurious amenities, and extensive grounds."
            },
            new ResidenceType
            {
                Name = "Chalet",
                Description = "A wooden house or cottage, typically found in mountain regions, often used for skiing vacations."
            },
            new ResidenceType
            {
                Name = "Serviced Apartment",
                Description = "An apartment that offers hotel-like services such as housekeeping and concierge."
            },
            new ResidenceType
            {
                Name = "Yurt",
                Description = "A portable, round tent traditionally used by nomads, now a unique lodging option in remote areas."
            },
            new ResidenceType
            {
                Name = "Houseboat",
                Description = "A floating residence on a river or lake, providing a unique waterfront living experience."
            },
            new ResidenceType
            {
                Name = "Treehouse",
                Description = "An elevated residence built in or around a tree, offering a nature-immersive experience."
            }
            ];
    }

}
