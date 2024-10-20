using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validations.Extensions;
using TravelAccommodationBookingPlatform.Domain.Constants;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Register;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.Username)
            .NotEmpty()
            .MaximumLength(DomainRules.Users.UsernameMaxLength)
            .WithMessage($"Username cannot be more than {DomainRules.Users.UsernameMaxLength} characters long.");

        RuleFor(command => command.Password)
            .NotEmpty()
            .MaximumLength(DomainRules.Users.PasswordMaxLength)
            .WithMessage($"Password cannot be more than {DomainRules.Users.PasswordMaxLength} characters long.");

        RuleFor(command => command.Email)
            .NotEmpty()
            .MaximumLength(DomainRules.Users.EmailMaxLength)
            .WithMessage($"Email cannot be more than {DomainRules.Users.EmailMaxLength} characters long.")
            .EmailAddress();

        RuleFor(command => command.PhoneNumber)
            .NotEmpty()
            .PhoneNumber();
    }

}
