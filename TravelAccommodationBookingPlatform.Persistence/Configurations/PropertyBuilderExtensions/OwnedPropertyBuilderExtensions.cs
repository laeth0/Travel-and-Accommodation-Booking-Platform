namespace TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

/// <summary>
/// <see cref="Review"/> is assumed to be an (owned) entity,
/// because some Entities (e.g. <see cref="Hotel"/>) own a collection of it.
/// EF Core doesn't support complex type collections yet.
/// </summary>
public static class OwnedPropertyBuilderExtensions { }