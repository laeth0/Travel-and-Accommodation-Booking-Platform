

using Booking.BLL.IService;
using Booking.DAL.ConfigModels;
using Booking.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Booking.BLL.Service
{
    public class ServiceManager : IServiceManager
    {

        private readonly Lazy<IEmailService> _emailService;
        private readonly Lazy<IFileService> _fileService;
        private readonly Lazy<ITokenService> _tokenService;

        public ServiceManager(IOptions<Email> email, IOptions<JWT> jwt, UserManager<ApplicationUser> userManager)
        {
            _emailService = new Lazy<IEmailService>(() => new EmailService(email));
            _fileService = new Lazy<IFileService>(() => new FileService());
            _tokenService = new Lazy<ITokenService>(() => new TokenService(jwt, userManager));
        }
        public IEmailService EmailService => _emailService.Value;
        public IFileService FileService => _fileService.Value;
        public ITokenService TokenService => _tokenService.Value;
    }
}
