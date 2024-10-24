using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Persistence.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

namespace TravelAccommodationBookingPlatform.Application.Features.Auth.RefreshTokens;
public class RefreshTokensCommandHandler : ICommandHandler<RefreshTokensCommand, RefreshTokensResponse>
{

    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RefreshTokensCommandHandler(
        UserManager<AppUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
    }



    public async Task<Result<RefreshTokensResponse>> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        var principal = _refreshTokenRepository.GetPrincipalFromToken(request.token);
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

        var storedRefreshToken = await _refreshTokenRepository.GetAsync(x => x.Value == request.refreshToken);

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

        var jwtId = principal.Value.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;


        if (storedRefreshToken.Value.JwtId.ToString() != jwtId)
        {
            return DomainErrors.Token.RefreshTokenDoesNotMatchJti;
        }

        storedRefreshToken.Value.IsRevoked = true;
        _refreshTokenRepository.Update(storedRefreshToken.Value);

        var userId = principal.Value.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var user = await _userManager.FindByIdAsync(userId);

        var jwtAccessToken = await _jwtTokenGenerator.GenerateToken(user);

        var refreshTokenValue = _refreshTokenRepository.GenerateRefreshToken();

        var RefreshTokenResult = user.AddToken(jwtAccessToken.Id, refreshTokenValue);

        return new RefreshTokensResponse(jwtAccessToken.Value, RefreshTokenResult);
    }








}
