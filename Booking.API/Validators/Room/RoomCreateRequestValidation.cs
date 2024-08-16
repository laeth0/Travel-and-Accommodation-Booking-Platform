using Booking.API.DTOs;
using FluentValidation;

namespace Booking.API.Validators;
public class RoomCreateRequestValidation : AbstractValidator<RoomCreateRequest>
{
    public RoomCreateRequestValidation()
    {
        RuleFor(x => x.AdultsCapacity)
            .GreaterThan(0);

        RuleFor(x => x.ChildrenCapacity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Number)
            .GreaterThan(0);

        RuleFor(x => x.PricePerNight)
            .GreaterThan(0);

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.Image)
            .NotEmpty();

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);

        RuleFor(x => x.ResidenceId)
            .NotEmpty();

        RuleFor(x => x.RoomTypeId)
            .NotEmpty();
    }
}
