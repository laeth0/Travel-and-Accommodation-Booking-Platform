namespace Booking.Domain.Interfaces.Services;
public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string message);
}
