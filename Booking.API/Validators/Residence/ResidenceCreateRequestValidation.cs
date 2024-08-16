


using Booking.API.DTOs;
using FluentValidation;

namespace Booking.API.Validators;
public class ResidenceCreateRequestValidation : AbstractValidator<ResidenceCreateRequest>
{

    public ResidenceCreateRequestValidation()
    {
        RuleFor(x => x.Name)
          .NotEmpty();


        RuleFor(x => x.Description)
          .NotEmpty();

        RuleFor(x => x.Address)
            .NotEmpty();

        RuleFor(x => x.Image)
            .NotNull();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty();

        RuleFor(x => x.FloorsNumber)
            .NotEmpty();

        RuleFor(x => x.Rating)
            .NotEmpty();

        RuleFor(x => x.CityId)
            .NotEmpty();

        RuleFor(x => x.ResidenceTypeId)
            .NotEmpty();
    }
}

