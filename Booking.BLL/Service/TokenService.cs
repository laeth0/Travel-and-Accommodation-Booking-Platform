using Booking.BLL.IService;
using Booking.DAL.ConfigModels;
using Booking.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Booking.BLL.Service;
public class TokenService(IOptions<JWT> jwt, UserManager<ApplicationUser> userManager) : ITokenService
{

    private readonly JWT _jwt = jwt.Value;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    private async Task<IEnumerable<Claim>> GetClams(ApplicationUser user)
    {
        var userClams = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);


        StringBuilder RolesOfUser = new StringBuilder();
        foreach (var role in userRoles)
            RolesOfUser.Append(role + ',');


        var claims = new[]
        {
            new Claim("Id", user.Id),
            new Claim("Name", user.UserName!),
            new Claim("Email", user.Email!),//ClaimTypes.Email
            new Claim("Roles", RolesOfUser.ToString()[..^1]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        }.Union(userClams);


        return claims;
    }

    public async Task<(string, DateTime)> GenerateToken(ApplicationUser user)
    {
        var claims = await GetClams(user);

        var key = Encoding.ASCII.GetBytes(_jwt.Key);
        var symmetricSecurityKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(_jwt.DurationInDays),
            SigningCredentials = signingCredentials,
            Audience = _jwt.Audience,
            Issuer = _jwt.Issuer,
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(securityToken);

        return (token, securityToken.ValidTo);

    }


    public string GetValueFromToken(string token, string key= "Roles")
    {
        //var JwtToken = token.Replace("Bearer ", ""); //  or use => var jwt = token[7..]; remove "Bearer " from the token
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        var value = jsonToken.Claims.First(claim => claim.Type == key).Value;
        return value;
    }

}
