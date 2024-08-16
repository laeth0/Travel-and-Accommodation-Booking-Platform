

using Booking.API.DTOs;
using FluentValidation;

namespace Booking.API.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FirstName)
          .NotEmpty();

        RuleFor(x => x.LastName)
          .NotEmpty();

        RuleFor(x => x.Email)
          .NotEmpty()
          .EmailAddress();

        RuleFor(x => x.Password)
          .NotEmpty();
    }
}