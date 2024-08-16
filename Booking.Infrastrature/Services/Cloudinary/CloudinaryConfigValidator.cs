


using FluentValidation;

namespace Booking.Infrastrature.Services;
public class CloudinaryConfigValidator : AbstractValidator<CloudinaryConfig>
{

    public CloudinaryConfigValidator()
    {
        RuleFor(x => x.CloudName)
            .NotEmpty();

        RuleFor(x => x.ApiKey)
            .NotEmpty();

        RuleFor(x => x.ApiSecret)
            .NotEmpty();
    }
}
