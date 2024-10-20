using FluentValidation;
using MediatR;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.Shared;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;
using TravelAccommodationBookingPlatform.Domain.Shared.ValidationResult;

namespace TravelAccommodationBookingPlatform.Application.PipelineBehavior;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) =>
        _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);


        var validationResults = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context, cancellationToken))
            );

        Error[] errors = validationResults.Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            //.Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(
                ErrorType.UnprocessableEntity,
                failure.PropertyName,
                failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Length == 0)
        {
            return await next();
        }

        return CreateValidationResult<TResponse>(errors);

    }

    private static TResult CreateValidationResult<TResult>(Error[] errors) where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}
