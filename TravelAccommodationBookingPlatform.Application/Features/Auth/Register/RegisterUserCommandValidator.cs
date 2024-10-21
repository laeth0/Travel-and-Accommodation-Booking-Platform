using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Validations.Extensions;
using TravelAccommodationBookingPlatform.Domain.Constants;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.Register;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator(IUnitOfWork unitOfWork)
    {

        RuleFor(command => command.PhoneNumber)
          .NotEmpty()
          .PhoneNumber();



        RuleFor(command => command.Password)
          .NotEmpty()
          .MaximumLength(DomainRules.Users.PasswordMaxLength)
          .WithMessage(errorMessage: $"Password cannot be more than {DomainRules.Users.PasswordMaxLength} characters long.");




        RuleFor(command => command.Username)
            .NotEmpty()
            .MaximumLength(DomainRules.Users.UsernameMaxLength)
            .WithMessage($"Username cannot be more than {DomainRules.Users.UsernameMaxLength} characters long.")
            .MustAsync(async (username, cancellationToken) =>
            {
                var user = await unitOfWork.AppUserRepository.GetAsync(u => u.UserName == username, cancellationToken);
                return user.HasNoValue;
            })
            .WithMessage(DomainErrors.User.UsernameAlreadyExists);



        RuleFor(command => command.Email)
            .NotEmpty()
            .MaximumLength(DomainRules.Users.EmailMaxLength)
            .WithMessage($"Email cannot be more than {DomainRules.Users.EmailMaxLength} characters long.")
            .EmailAddress()
            .MustAsync(async (email, cancellationToken) =>
            {
                var user = await unitOfWork.AppUserRepository.GetAsync(u => u.Email == email, cancellationToken);
                return user.HasNoValue;
            })
            .WithMessage(DomainErrors.User.EmailAlreadyExists);

    }

}
