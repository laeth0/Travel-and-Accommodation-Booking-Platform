namespace TravelAccommodationBookingPlatform.Api.ServiceCollectionExtention;

public interface IServiceInstaller
{
    IServiceCollection Install(IServiceCollection services, IConfiguration configuration);
}
