using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Domain.ValueObjects;
public record ActivationCode
{
    public string Value { get; set; }
    public DateTime ExpiresAtUtc { get; set; }

    public ActivationCode(string value, DateTime expiresAtUtc)
    {
        Value = value;
        ExpiresAtUtc = expiresAtUtc;
    }

    public static ActivationCode Create(string value, DateTime expiresAtUtc)
    {
        Ensure.NotEmpty(value, "The activation code is required.", nameof(value));
        Ensure.NotDefault(expiresAtUtc, "The expiration date is required.", nameof(expiresAtUtc));
        Ensure.NotPast(expiresAtUtc, "The expiration date must be in the future.", nameof(expiresAtUtc));

        return new ActivationCode(value, expiresAtUtc);
    }


}
