﻿

using Booking.Domain.Interfaces.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;


namespace Booking.Infrastrature.Services;
public class EmailService(IOptions<EmailConfig> email) : IEmailService
{
    private readonly EmailConfig _email = email.Value;


    private MimeMessage CreateEmailMessage(string toEmail, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(MailboxAddress.Parse(_email.FromEmail));

        emailMessage.To.Add(new MailboxAddress(toEmail.Split('@')[0], toEmail));
        // if i want send to multiple emails  emailMessage.To.AddRange(emailRequest.ToEmails.Select(MailboxAddress.Parse));

        emailMessage.Subject = subject;

        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
        {
            Text = message
        };

        return emailMessage;
    }


    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {

        using var client = new SmtpClient();

        try
        {
            await client.ConnectAsync(_email.SmtpServer, _email.Port, _email.UseSSl);
            //client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_email.FromEmail, _email.Password);

            await client.SendAsync(CreateEmailMessage(toEmail, subject, message));
        }
        catch (Exception ex)
        {

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"---Error----->: {ex.Message}");
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }


    }
}
