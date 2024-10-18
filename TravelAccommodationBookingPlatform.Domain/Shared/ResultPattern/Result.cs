namespace TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        (IsSuccess, Error) = isSuccess switch
        {
            true when error != Error.None => throw new InvalidOperationException(),
            false when error == Error.None => throw new InvalidOperationException(),
            _ => (isSuccess, error),
        };
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }


    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);


    public static implicit operator Result(Error error) => Failure(error);

}

