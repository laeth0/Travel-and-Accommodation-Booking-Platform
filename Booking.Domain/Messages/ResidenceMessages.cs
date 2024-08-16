




namespace Booking.Domain.Messages;

public static class ResidenceMessages
{
    public const string NotFound = "Residence with the given ID is not found.";
    public const string SameLocationExists = "A Residence in the same location (longitude, latitude) already exists.";
    public const string DependentsExist = "The specified Residence still has dependents attached to it.";
    public const string ResidenceCreated = "Residence Created Successfully";
    public const string ResidenceUpdated = "Residence Updated Successfully";
    public const string ResidenceDeleted = "Residence Deleted  Successfully";

    public static string ResidenceExist(string ResidenceName = "")
    {
        return $"the {ResidenceName} Residence is Exist in the system";
    }

    public static string ResidenceNotExist(string ResidenceName = "")
    {
        return $"the {ResidenceName} Residence is not Exist in the system";
    }

}