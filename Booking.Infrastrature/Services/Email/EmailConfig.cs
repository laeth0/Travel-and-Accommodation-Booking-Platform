


namespace Booking.Infrastrature.Services;
public class EmailConfig
{
    // in the option classes the property must be public and have a getter and setter 
    // and has public constructor with no parameters (it exist by default in the class)
    public required string FromEmail { get; set; }
    public required string Password { get; set; }
    public required string SmtpServer { get; set; }
    public required int Port { get; set; }
    public required bool UseSSl { get; set; }
    public required string DisplayName { get; set; }
}
