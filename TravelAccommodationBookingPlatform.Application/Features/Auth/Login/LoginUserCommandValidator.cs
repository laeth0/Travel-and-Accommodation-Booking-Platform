using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command => command.Email)
            .MaximumLength(DomainRules.Users.EmailMaxLength)
            .WithMessage($"Email cannot be more than {DomainRules.Users.EmailMaxLength} characters long.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MaximumLength(DomainRules.Users.PasswordMaxLength)
            .WithMessage($"Password cannot be more than {DomainRules.Users.PasswordMaxLength} characters long.");

    }
}