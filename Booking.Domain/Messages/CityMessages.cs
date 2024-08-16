


namespace Booking.Domain.Messages;

public static class CityMessages
{
    public const string NotFound = "City with the given ID is not found.";
    public const string DependentsExist = "The specified city still has dependents attached to it.";
    public const string CityCreated = "City Created Successfully";
    public const string CityUpdated = "City Updated Successfully";
    public const string CityDeleted = "City Deleted  Successfully";

    public static string CityExist(string CityName = "")
    {
        return $"the {CityName} City is Exist in the system";
    }

    public static string CityNotExist(string CityName = "")
    {
        return $"the {CityName} City is not Exist in the system";
    }


}