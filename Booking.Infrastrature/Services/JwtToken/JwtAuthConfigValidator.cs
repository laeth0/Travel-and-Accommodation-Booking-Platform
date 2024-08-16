



using FluentValidation;

namespace Booking.Infrastrature.Services;
public class JwtAuthConfigValidator : AbstractValidator<JwtAuthConfig>
{
    public JwtAuthConfigValidator()
    {
        RuleFor(x => x.Key)
          .NotEmpty();

        RuleFor(x => x.Issuer)
          .NotEmpty();

        RuleFor(x => x.Audience)
          .NotEmpty();

        RuleFor(x => x.DurationInDays)
          .NotEmpty()
          .GreaterThan(0);

    }
}