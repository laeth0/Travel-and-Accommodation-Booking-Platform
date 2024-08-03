using Booking.BLL.IService;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;


namespace Ecommerce.Presentation.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {

            var FromEmail = _configuration["EmailSettings:FromEmail"];
            var SmtpServer = _configuration["EmailSettings:SmtpServer"];
            var Port = int.Parse(_configuration["EmailSettings:Port"]!); // i add ! to suppress nullable warning
            var UseSSl = bool.Parse(_configuration["EmailSettings:UseSSl"]!); // i add ! to told the compiler that this value is not null
            var Password = _configuration["EmailSettings:Password"];


            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Ecommerce Admin", FromEmail));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(SmtpServer, Port, UseSSl);
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(FromEmail, Password);
                    await client.SendAsync(emailMessage);
                }
                catch (Exception ex)
                {

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"---Error----->: {message}");
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }



        }
    }
}
