using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace TABP.Application.Extensions.Validation;

/// <summary>
///   Extension methods for FluentValidation to add custom validation rules.
/// </summary>
public static class ValidationExtensions
{


    /// <summary>
    ///   Validates that the string property represents a valid phone number.
    /// </summary>
    /// <typeparam name="T">The type of the object being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder.</param>
    /// <returns>The rule builder options.</returns>
    /// <remarks>
    ///   The phone number pattern allows various formats including international codes, area codes, and optional separators.
    /// </remarks>
    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(
      this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
          .Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")
          .WithMessage(ValidationMessages.PhoneNumberIsNotValid);
    }



}