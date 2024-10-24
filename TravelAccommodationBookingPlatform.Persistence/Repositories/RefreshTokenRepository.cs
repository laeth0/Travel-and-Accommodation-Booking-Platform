using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using TravelAccommodationBookingPlatform.Application.Interfaces.Persistence.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared.MaybePattern;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;
public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
{
    private bool _disposed = false;
    private readonly TokenValidationParameters _tokenValidationParameters;


    public RefreshTokenRepository(AppDbContext context, TokenValidationParameters tokenValidationParameters) : base(context)
    {
        _tokenValidationParameters = tokenValidationParameters;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public Maybe<ClaimsPrincipal> GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
            if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
            {
                return Maybe<ClaimsPrincipal>.None;
            }
            return principal;
        } catch
        {
            return Maybe<ClaimsPrincipal>.None;
        }
    }

    private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        => (validatedToken is JwtSecurityToken jwtSecurityToken) &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                   StringComparison.InvariantCultureIgnoreCase);



    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _dbContext?.Dispose();
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~RefreshTokenRepository()
    {
        Dispose(false);
    }
}
