

namespace Booking.API.DTOs;
public class RegisterRequest
{
  public string FirstName { get; init; }
  public string LastName { get; init; }
  public string UserName { get; init; }
  public string Email { get; init; }
  public string Password { get; init; }
}