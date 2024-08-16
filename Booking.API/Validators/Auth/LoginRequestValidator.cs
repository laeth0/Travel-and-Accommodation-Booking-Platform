

using Booking.API.DTOs;
using FluentValidation;

namespace Booking.API.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
          .NotEmpty()
          .NotNull()
          .EmailAddress();


        RuleFor(x => x.Password)
          .NotEmpty()
          .NotNull();
    }
}