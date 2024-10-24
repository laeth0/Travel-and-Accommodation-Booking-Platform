using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;
using TravelAccommodationBookingPlatform.Domain.Shared.ValidationResult;

namespace TravelAccommodationBookingPlatform.Application.Shared.Extensions;

public static class ProblemDetailsExtension
{

    public static ObjectResult ToProblemDetails(this Result result)
    {
        return result.Error.Type switch
        {
            ErrorType.BadRequest => result.ToBadRequestProblemDetails(),
            ErrorType.NotFound => result.ToNotFoundProblemDetails(),
            ErrorType.NotAuthorized => result.ToUnauthorizedProblemDetails(),
            ErrorType.Conflict => result.ToConflictProblemDetails(),
            ErrorType.InternalServerError => result.ToInternalServerErrorProblemDetails(),
            ErrorType.Forbidden => result.ToForbiddenProblemDetails(),
            ErrorType.TooManyRequests => result.ToTooManyRequestsProblemDetails(),
            ErrorType.UnprocessableEntity => result.ToUnprocessableEntityProblemDetails(),
            ErrorType.None => throw new InvalidOperationException(
                $"Cannot create problem details for the successful result '{result}'"),
            _ => throw new ArgumentOutOfRangeException(nameof(result.Error.Type) + " undefined type")
        };
    }

    private static BadRequestObjectResult ToBadRequestProblemDetails(this Result result)
    {
        return new BadRequestObjectResult(CreateProblemDetails(result, StatusCodes.Status400BadRequest));
    }

    private static NotFoundObjectResult ToNotFoundProblemDetails(this Result result)
    {
        return new NotFoundObjectResult(CreateProblemDetails(result, StatusCodes.Status404NotFound));
    }

    private static UnauthorizedObjectResult ToUnauthorizedProblemDetails(this Result result)
    {
        return new UnauthorizedObjectResult(CreateProblemDetails(result, StatusCodes.Status401Unauthorized));
    }

    private static ConflictObjectResult ToConflictProblemDetails(this Result result)
    {
        return new ConflictObjectResult(CreateProblemDetails(result, StatusCodes.Status409Conflict));
    }

    private static ObjectResult ToInternalServerErrorProblemDetails(this Result result)
    {
        return new ObjectResult(CreateProblemDetails(result, StatusCodes.Status500InternalServerError))
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }

    private static ObjectResult ToForbiddenProblemDetails(this Result result)
    {
        return new ObjectResult(CreateProblemDetails(result, StatusCodes.Status403Forbidden))
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
    }

    private static ObjectResult ToTooManyRequestsProblemDetails(this Result result)
    {
        return new ObjectResult(CreateProblemDetails(result, StatusCodes.Status429TooManyRequests))
        {
            StatusCode = StatusCodes.Status429TooManyRequests
        };
    }

    private static UnprocessableEntityObjectResult ToUnprocessableEntityProblemDetails(this Result result)
    {
        var problemDetails = CreateProblemDetails(result, StatusCodes.Status422UnprocessableEntity);

        problemDetails.Extensions.Add("errors", (result as IValidationResult)?.Errors.Select(err => new { err.Code, err.Message }));

        return new UnprocessableEntityObjectResult(problemDetails);
    }

    private static ProblemDetails CreateProblemDetails(Result result, int statusCode)
    {

        var errorResponse = new ProblemDetails()
        {
            Status = statusCode,
            Detail = $"{result.Error.Code} , {result.Error.Message}"
        };

        var problemHttpResult = TypedResults.Problem(errorResponse);

        return problemHttpResult.ProblemDetails;
    }
}