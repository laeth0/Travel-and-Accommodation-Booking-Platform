using Booking.API.DTOs;
using FluentValidation;

namespace Booking.API.Validators;
public class CountryCreateRequestValidation : AbstractValidator<CountryCreateRequest>
{

    public CountryCreateRequestValidation()
    {
        RuleFor(x => x.Name)
          .NotEmpty();


        RuleFor(x => x.Description)
          .NotEmpty()
          .NotNull();

        RuleFor(x => x.Image)
            .NotNull();
    }
}
