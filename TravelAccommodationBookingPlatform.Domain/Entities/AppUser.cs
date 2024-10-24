using Microsoft.AspNetCore.Identity;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class AppUser : IdentityUser
{

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    private readonly List<RefreshToken> _tokens = new();

    public virtual IReadOnlyCollection<RefreshToken> Tokens => _tokens.AsReadOnly();

    public virtual ActivationCode ActivationCode { get; set; }

    public bool IsActive { get; private set; } = false;

    public Result Activate()
    {
        if (IsActive)
        {
            return DomainErrors.User.UserAlreadyActive;
        }

        IsActive = true;
        ActivationCode.Value = string.Empty;
        ActivationCode.ExpiresAtUtc = DateTime.MinValue;

        return Result.Success();
    }

    public string AddToken(Guid jwtId, string? tokenValue = null)
    {
        tokenValue ??= Guid.NewGuid().ToString();

        var token = new RefreshToken
        {
            Value = tokenValue,
            ExpiresAt = DateTime.UtcNow.AddMonths(1),
            AppUserId = Id,
            JwtId = jwtId
        };


        _tokens.Add(token);
        return token.Value;

    }

    public void RemoveToken(string tokenValue)
    {
        var token = _tokens.FirstOrDefault(t => t.Value == tokenValue);

        if (token is { })
        {
            _tokens.Remove(token);
        }
    }





}
