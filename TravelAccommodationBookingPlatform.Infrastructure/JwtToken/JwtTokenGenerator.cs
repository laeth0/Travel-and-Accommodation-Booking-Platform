using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Models;

namespace TravelAccommodationBookingPlatform.Infrastructure.JwtToken;
public class JwtTokenGenerator : IJwtTokenGenerator
{

    private readonly JwtAuthConfig _jwt;
    private readonly UserManager<AppUser> _userManager;

    public JwtTokenGenerator(IOptionsMonitor<JwtAuthConfig> jwt, UserManager<AppUser> userManager)
    {

        _jwt = jwt.CurrentValue ?? throw new ArgumentException(nameof(JwtAuthConfig) + " not found");
        _userManager = userManager;
    }

    private async Task<IEnumerable<Claim>> GetClams(AppUser user, Guid TokenId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id), //ClaimTypes.NameIdentifier  is unique name for the user (لليوزر id عادة بكون  ) => it appear as nameid
            new Claim( ClaimTypes.Name, user.UserName!), // it appear as unique_name
            new Claim( ClaimTypes.Email, user.Email!), // it appear as email
            new Claim(ClaimTypes.Expiration, DateTime.UtcNow.Add(_jwt.ExpiryTimeFrame).ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, TokenId.ToString()),
        };

        var userClams = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        claims.AddRange(userClams);

        return claims;
    }


    public async Task<Jwt> GenerateToken(AppUser user)
    {
        Guid TokenId = Guid.NewGuid();

        var claims = await GetClams(user, TokenId);

        var key = Encoding.ASCII.GetBytes(_jwt.Key);
        var symmetricSecurityKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = signingCredentials,
            Audience = _jwt.Audience,
            Issuer = _jwt.Issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_jwt.ExpiryTimeFrame)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = new Jwt
        {
            Id = TokenId,
            Value = tokenHandler.WriteToken(securityToken),
            ExpiryDate = securityToken.ValidTo,
        };

        return token;

    }

}
