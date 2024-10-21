using System.Text.Json;
using FluentValidation;

namespace TravelAccommodationBookingPlatform.Application.Validations.Extensions;


/// <summary>
///   Extension methods for FluentValidation to add custom validation rules.
/// </summary>
public static class ValidationExtensions
{

    /// <summary>
    ///   Validates that the string property represents a strong password.
    /// </summary>
    /// <typeparam name="T">The type of the object being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder.</param>
    /// <returns>The rule builder options.</returns>
    /// <remarks>
    ///   The strong password pattern enforces a combination of uppercase and lowercase letters,
    ///   digits, and special characters, with a minimum length of 8 characters.
    /// </remarks>
    public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilderOptions<T, string> ruleBuilder)
    {
        return ruleBuilder
          .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
          .WithMessage("The password must contain at least 1 uppercase letter, 1 lowercase letter, 1 digit, 1 special character, and be at least 8 characters long.");
    }



    /// <summary>
    ///   Validates that the string property represents a valid phone number.
    /// </summary>
    /// <typeparam name="T">The type of the object being validated.</typeparam>
    /// <param name="ruleBuilder">The rule builder.</param>
    /// <returns>The rule builder options.</returns>
    /// <remarks>
    ///   The phone number pattern allows various formats including international codes, area codes, and optional separators.
    /// </remarks>
    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
          .Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")
          .WithMessage(ValidationMessages.PhoneNumberIsNotValid);
    }


    public static IRuleBuilderOptions<T, string> ValidJson<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
          .Must(IsValidJson)
          .WithMessage("The provided JSON is not valid. Please provide a valid JSON object.");
    }


    private static bool IsValidJson(string json)
    {
        try
        {
            _ = JsonDocument.Parse(json);

            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }



}