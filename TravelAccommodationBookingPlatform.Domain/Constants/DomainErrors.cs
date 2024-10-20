using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Domain.Constants;

public static class DomainErrors
{
    public static class General
    {
        public static Error ServerError => new(ErrorType.BadRequest, "General.ServerError", "The server encountered an unrecoverable error.");
        public static Error NotNull => new(ErrorType.BadRequest, "General.NotNull", "The value is not null.");
        public static Error NotEmpty => new(ErrorType.BadRequest, "General.NotEmpty", "The value is not empty.");
        public static Error NotDefault => new(ErrorType.BadRequest, "General.NotDefault", "The value is not default.");
        public static Error NotFuture => new(ErrorType.BadRequest, "General.NotFuture", "The value is not in the future.");
        public static Error NotPast => new(ErrorType.BadRequest, "General.NotPast", "The value is not in the past.");
        public static Error UnProcessableRequest => new(ErrorType.BadRequest, "General.UnProcessableRequest", "The request is unprocessable.");
        public static Error Unauthorized => new(ErrorType.BadRequest, "General.Unauthorized", "Unauthorized.");
        public static Error Of => new(ErrorType.BadRequest, "General.Of", "The value is of the wrong type.");
        public static Error Between => new(ErrorType.BadRequest, "General.Between", "The value is not between the specified range.");
        public static Error IsTrue => new(ErrorType.BadRequest, "General.IsTrue", "The value is not true.");
        public static Error LessThan => new(ErrorType.BadRequest, "General.LessThan", "The value is not less than the specified value.");
        public static Error GreaterThan => new(ErrorType.BadRequest, "General.GreaterThan", "The value is not greater than the specified value.");
    }

    public static class User
    {
        public static readonly Error InvalidToken = new(
            ErrorType.BadRequest,
            "User.InvalidToken",
            "The token is invalid.");

        public static readonly Error ActivationCodeExpired = new(
            ErrorType.BadRequest,
            "User.ActivationCodeExpired",
            "The activation code has expired.");


        public static readonly Error UserNotFound = new(
            ErrorType.NotFound,
            "User.NotFound",
            "User not found.");


        public static readonly Error EmailOrPasswordIncorrect = new(
            ErrorType.NotAuthorized,
            "User.EmailOrPasswordIncorrect",
            "The email or password is incorrect.");


        public static readonly Error UsernameNotFound = new(
            ErrorType.NotAuthorized,
            "User.UsernameNotFound",
            "User with the given username does not exist");

        public static readonly Error InvalidCredentials = new(
            ErrorType.NotAuthorized,
            "User.InvalidCredentials",
            "The given credentials were invalid");

        public static readonly Error UsernameAlreadyExists = new(
            ErrorType.Conflict,
            "User.UsernameAlreadyExists",
            "User with the given username already exists");

        public static readonly Error EmailAlreadyExists = new(
            ErrorType.Conflict,
            "User.EmailAlreadyExists",
            "User with the given email already exists");

        public static readonly Error CredentialsNotProvided = new(
            ErrorType.NotAuthorized,
            "User.Unauthorized",
            "No user credentials were provided");

        public static readonly Error InvalidRole = new(
            ErrorType.Forbidden,
            "User.InvalidRole",
            "User does not have the required role(s)");

        public static readonly Error CannotDeleteUserWithBookings = new(
            ErrorType.Conflict,
            "User.CannotDeleteUserWithBookings",
            "Cannot delete user as they have existing bookings");

        public static readonly Error UserNotActivated = new(
            ErrorType.BadRequest,
            "User.NotActivated",
            "The user account is not activated.");

        public static Error UserAlreadyExists = new(
            ErrorType.BadRequest,
            "User.AlreadyExist",
            "User already exists.");

        public static Error UserAlreadyActive = new(
            ErrorType.BadRequest,
            "User.AlreadyActive",
            "User is already active.");


    }

    public static class Hotel
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "Hotel.IdNotFound",
            "Hotel with the given ID does not exist");

        public static readonly Error CannotDeleteHotelWithBookings = new(
            ErrorType.Conflict,
            "Hotel.CannotDeleteHotelWithBookings",
            "Cannot delete hotel as it has existing bookings");
    }

    public static class Room
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "Room.IdNotFound",
            "Room with the given ID does not exist");

        public static readonly Error CannotDeleteRoomWithBookings = new(
            ErrorType.Conflict,
            "Room.CannotDeleteRoomWithBookings",
            "Cannot delete room as it has existing bookings");

        public static readonly Error RoomNumberAlreadyExists = new(
            ErrorType.Conflict,
            "Room.RoomNumberAlreadyExists",
            "Room with this RoomNumber already exists");
    }

    public static class City
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "City.IdNotFound",
            "City with the given ID does not exist");

        public static readonly Error CannotDeleteCityWithHotels = new(
            ErrorType.Conflict,
            "City.CannotDeleteCityWithHotels",
            "Cannot delete city as it has existing hotels");
    }

    public static class Payment
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "Payment.IdNotFound",
            "Payment with the given ID does not exist");

        public static readonly Error CannotDeletePaymentWithBooking = new(
            ErrorType.Conflict,
            "Payment.CannotDeletePaymentWithBooking",
            "Cannot delete payment as it is associated with an existing booking");
    }

    public static class Booking
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "Booking.IdNotFound",
            "Booking with the given ID does not exist");

        public static readonly Error PaymentNotFound = new(
            ErrorType.NotFound,
            "Booking.PaymentNotFound",
            "Payment for the booking with the given ID was not found");

        public static readonly Error InvalidBookingOperation = new(
            ErrorType.Conflict,
            "Booking.InvalidBookingOperation",
            "This booking operation is not allowed due to conflicts");

        public static readonly Error RoomsNotAvailable = new(
            ErrorType.Conflict,
            "Booking.RoomsNotAvailable",
            "One or more of the booking rooms are not available in the given checking time");

        public static readonly Error CannotDeleteBookingWithPayment = new(
            ErrorType.Conflict,
            "Booking.CannotDeleteBookingWithPayment",
            "Cannot delete booking as it has an associated payment");
    }

    public static class Discount
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "Discount.IdNotFound",
            "Discount with the given ID does not exist");

        public static readonly Error CannotDeleteDiscountAppliedToBooking = new(
            ErrorType.Conflict,
            "Discount.CannotDeleteDiscountAppliedToBooking",
            "Cannot delete discount as it is applied to an existing booking");
    }

}