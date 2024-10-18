
using Microsoft.Extensions.Options;
using MimeKit;
using TravelAccommodationBookingPlatform.Application.Interfaces;

namespace TravelAccommodationBookingPlatform.Infrastructure.Email;
public class EmailService(IOptionsMonitor<EmailConfig> email) : IEmailService
{
    private readonly EmailConfig _email = email.CurrentValue;


    private MimeMessage CreateEmailMessage(string mailTo, string subject, string message)
    {
        var emailMessage = new MimeMessage()
        {
            Sender = MailboxAddress.Parse(_email.FromEmail),
            Subject = subject
        };

        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
        {
            Text = message
        };

        emailMessage.From.Add(new MailboxAddress(_email.DisplayName, _email.FromEmail));
        emailMessage.To.Add(MailboxAddress.Parse(mailTo));

        return emailMessage;
    }


    public async Task SendEmailAsync(string mailTo, string subject, string message)
    {
        var emailMessage = CreateEmailMessage(mailTo, subject, message);

        using var smtpClient = new MailKit.Net.Smtp.SmtpClient();

        await smtpClient.ConnectAsync(_email.Host, _email.Port, MailKit.Security.SecureSocketOptions.StartTls);

        await smtpClient.AuthenticateAsync(_email.FromEmail, _email.Password);

        await smtpClient.SendAsync(emailMessage);

        await smtpClient.DisconnectAsync(true);
    }
}
