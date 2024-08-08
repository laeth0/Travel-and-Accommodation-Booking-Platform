


namespace Booking.DAL.ConfigModels;
public class Email
{
    // in the option classes the property must be public and have a getter and setter 
    // and has public constructor with no parameters (it exist by default in the class)
    public string FromEmail { get; set; } 
    public string Password { get; set; } 
    public string SmtpServer { get; set; } 
    public int Port { get; set; }
    public bool UseSSl { get; set; }
    public string DisplayName { get; set; } 
}
