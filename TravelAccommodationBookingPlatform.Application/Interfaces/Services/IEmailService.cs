namespace TravelAccommodationBookingPlatform.Application.Interfaces;
public interface IEmailService : ITransientService
{
    Task SendEmailAsync(string mailTo, string subject, string message);
}

