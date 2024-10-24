using System.Text.Json;
using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Domain.Models;

namespace TravelAccommodationBookingPlatform.Application.Shared.Extensions;
public static class ResponseHeadersExtensions
{
    public static void AddPaginationMetadata(this IHeaderDictionary headers, PaginationMetadata paginationMetadata)
    {
        headers["x-pagination"] = JsonSerializer.Serialize(paginationMetadata);


        // this is an Example for using this extension method
        //Response.Headers.AddPaginationMetadata(amenities.PaginationMetadata);

    }
}