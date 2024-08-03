

namespace Booking.BLL.IService
{
    public interface IServiceManager
    {
        IEmailService EmailService { get; }
        IFileService FileService { get; }
        ITokenService TokenService { get; }
    }
}
