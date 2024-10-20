using FluentValidation;

namespace TravelAccommodationBookingPlatform.Api.RateLimiting;

public class FixedWindowRateLimiterConfigValidator : AbstractValidator<FixedWindowRateLimiterConfig>
{
    public FixedWindowRateLimiterConfigValidator()
    {
        RuleFor(x => x.PermitLimit)
          .NotNull()
          .GreaterThanOrEqualTo(0);

        RuleFor(x => x.TimeWindowSeconds)
          .NotNull()
          .GreaterThanOrEqualTo(0);

        RuleFor(x => x.QueueLimit)
          .NotNull()
          .GreaterThanOrEqualTo(0);

        RuleFor(x => x.QueueProcessingOrder)
          .NotNull()
          .IsInEnum();

        RuleFor(x => x.AutoReplenishment)
            .NotNull();
    }
}