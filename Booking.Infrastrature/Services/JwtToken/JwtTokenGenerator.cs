using Booking.Domain.Entities;
using Booking.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Booking.Infrastrature.Services;
public class JwtTokenGenerator(IOptions<JwtAuthConfig> jwt, UserManager<ApplicationUser> userManager) : IJwtTokenGenerator
{

    private readonly JwtAuthConfig _jwt = jwt.Value;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    private async Task<IEnumerable<Claim>> GetClams(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id), //ClaimTypes.NameIdentifier  is unique name for the user (لليوزر id عادة بكون  ) => it appear as nameid
            new Claim( ClaimTypes.Name, user.UserName!), // it appear as unique_name
            new Claim( ClaimTypes.Email, user.Email!), // it appear as email
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var userClams = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
        claims.AddRange(userClams);

        return claims;
    }


    // this is value tuple (implicit type) => (string, DateTime)
    // if you want to use explicit type you can use => ValueTuple<string, DateTime>
    // or explicit names => (string token, DateTime expiration)
    public async Task<(string, DateTime)> GenerateToken(ApplicationUser user)
    {
        var claims = await GetClams(user);

        var key = Encoding.ASCII.GetBytes(_jwt.Key);
        var symmetricSecurityKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        //  services هو يلي بيحمل معلومات التوكن ومعلومات اليوزر عشان لما اخلق توكن اخلقها بنفس المعلومات يلي حددتها في SecurityTokenDescriptor ال     
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = signingCredentials, //  توقيع أوراق الاعتماد
            Audience = _jwt.Audience,
            Issuer = _jwt.Issuer,
            Expires = DateTime.UtcNow.AddDays(_jwt.DurationInDays),
            // لحد هون انا حددت معلومات التوكن ولكن ما وضعت فيها معلومات عن اليوزر
            Subject = new ClaimsIdentity(claims),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(securityToken);

        return (token, securityToken.ValidTo);

    }


    public string GetValueFromToken(string token, string key = "Roles")
    {
        //  var JwtToken = token.Replace("Bearer ", "");
        // or ure=> var JwtToken = token["Bearer ".Length..]; // remove "Bearer " from the token
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        var value = jsonToken?.Claims.First(claim => claim.Type == key).Value;
        return value!;
    }

}
