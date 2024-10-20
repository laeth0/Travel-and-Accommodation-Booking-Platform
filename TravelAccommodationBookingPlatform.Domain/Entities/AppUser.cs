using Microsoft.AspNetCore.Identity;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Domain.Entities;
public class AppUser : IdentityUser
{

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    private readonly List<Token> _tokens = new();

    public virtual IReadOnlyCollection<Token> Tokens => _tokens.AsReadOnly();

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

    public void AddToken(string tokenValue, DateTime expiresAt)
    {
        var token = new Token
        {
            Value = tokenValue,
            ExpiresAt = expiresAt,
            AppUserId = Id
        };

        _tokens.Add(token);
    }

    public void ClearToken(string tokenValue)
    {
        var token = _tokens.FirstOrDefault(t => t.Value == tokenValue);

        if (token is { })
        {
            _tokens.Remove(token);
        }
    }





}
