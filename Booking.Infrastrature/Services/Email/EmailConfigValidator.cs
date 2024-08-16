


using FluentValidation;

namespace Booking.Infrastrature.Services;

public sealed class EmailConfigValidator : AbstractValidator<EmailConfig>
{
    public EmailConfigValidator()
    {
        RuleFor(x => x.FromEmail)
          .NotEmpty()
          .EmailAddress();

        RuleFor(x => x.Password)
          .NotEmpty();

        RuleFor(x => x.SmtpServer)
            .NotEmpty();

        RuleFor(x => x.Port)
          .GreaterThan(0);

        RuleFor(x => x.UseSSl)
            .NotNull();

        RuleFor(x => x.DisplayName)
            .NotEmpty();

    } 
}
