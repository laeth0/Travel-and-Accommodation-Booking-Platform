using Booking.API.DTOs;
using FluentValidation;

namespace Booking.API.Validators;
public class CityCreateRequestValidation : AbstractValidator<CityCreateRequest>
{

    public CityCreateRequestValidation()
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
