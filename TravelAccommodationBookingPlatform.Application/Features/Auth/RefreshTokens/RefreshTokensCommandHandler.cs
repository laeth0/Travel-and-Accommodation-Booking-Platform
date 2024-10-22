using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared.MaybePattern;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.RefreshTokens;
public class RefreshTokensCommandHandler : ICommandHandler<RefreshTokensCommand, RefreshTokensResponse>
{

    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokensCommandHandler(TokenValidationParameters tokenValidationParameters,
        UserManager<AppUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _tokenValidationParameters = tokenValidationParameters;
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }



    public async Task<Result<RefreshTokensResponse>> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        var principal = GetPrincipalFromToken(request.token);
        if (principal.HasNoValue)
        {
            return DomainErrors.Token.CannotGetPrincipalFromToken;
        }

        var expiryDateUnix = long.Parse(principal.Value.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);


        // TODO: Check if the token has expired
        var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddSeconds(expiryDateUnix);

        if (expiryDateTimeUtc > DateTime.UtcNow)
        {
            return DomainErrors.Token.TokenhasntExpiredYet;
        }

        var jti = principal.Value.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        var storedRefreshToken = await _unitOfWork.TokenRepository.GetAsync(x => x.Value == request.refreshToken);

        if (storedRefreshToken == null)
        {
            return DomainErrors.Token.RefreshTokenNotFound;
        }

        if (DateTime.UtcNow > storedRefreshToken.Value.ExpiresAt)
        {
            return DomainErrors.Token.RefreshTokenExpired;
        }

        if (storedRefreshToken.Value.IsRevoked)
        {
            return DomainErrors.Token.RefreshTokenRevoked;
        }

        if (storedRefreshToken.Value.JwtId.ToString() != jti)
        {
            return DomainErrors.Token.RefreshTokenDoesNotMatchJti;
        }

        storedRefreshToken.Value.IsRevoked = true;
        _unitOfWork.TokenRepository.Update(storedRefreshToken.Value);
        await _unitOfWork.SaveChangesAsync();

        var userId = principal.Value.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var user = await _userManager.FindByIdAsync(userId);

        var tokenResult = await _jwtTokenGenerator.GenerateToken(user);

        var RefreshTokenResult = user.AddToken(tokenResult.Id);


        return new RefreshTokensResponse(tokenResult.Value, RefreshTokenResult);
    }



    private Maybe<ClaimsPrincipal> GetPrincipalFromToken(string token)
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
        }
        catch
        {
            return Maybe<ClaimsPrincipal>.None;
        }
    }

    private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        => (validatedToken is JwtSecurityToken jwtSecurityToken) &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                   StringComparison.InvariantCultureIgnoreCase);





}
