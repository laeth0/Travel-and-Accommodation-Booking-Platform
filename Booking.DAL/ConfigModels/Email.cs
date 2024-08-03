


namespace Booking.DAL.ConfigModels;
public class Email
{
    public string FromEmail { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string SmtpServer { get; set; } = null!;
    public int Port { get; set; }
    public bool UseSSl { get; set; }
    public string DisplayName { get; set; } = null!;
}
