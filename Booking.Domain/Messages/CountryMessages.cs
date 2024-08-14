


namespace Booking.Domain.Messages;
public static class CountryMessages
{
    public const string CountryCreated = "Country Created Successfully";
    public const string CountryUpdated = "Country Updated Successfully";
    public const string CountryDeleted = "Country Deleted  Successfully";

    public static string CountryExist(string countryName = "")
    {
        return $"the {countryName} Country is Exist in the system";
    }

    public static string CountryNotExist(string countryName = "")
    {
        return $"the {countryName} Country is not Exist in the system";
    }





}
