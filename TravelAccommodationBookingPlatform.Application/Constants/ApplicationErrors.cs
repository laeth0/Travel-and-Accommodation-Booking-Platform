using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Constants;
public static class ApplicationErrors
{
    public static class File
    {
        public static Error UploadFailed(string message = "") => new(
            ErrorType.InternalServerError,
            "File.UploadFailed",
            "An error occurred while uploading the file" + message);

        public static Error DeletionFailed(string message = "") => new(
            ErrorType.InternalServerError,
            "File.DeletionFailed",
            "An error occurred while deleting the file" + message);
    }
}