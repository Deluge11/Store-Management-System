using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Options;
using DTOs;
using Business_Layer.Business;

namespace Presentation_Layer.Authentication;

public class AuthenticateHelper
{
    public JwtOptions JwtOptions { get; }
    public AuthorizeBusiness AuthorizeBusiness { get; }

    public AuthenticateHelper(JwtOptions jwtOptions, AuthorizeBusiness authorizeBusiness)
    {
        JwtOptions = jwtOptions;
        AuthorizeBusiness = authorizeBusiness;
    }


    public async Task<string> CreateToken(int accountId)
    {
        if(accountId < 1)
        {
            return string.Empty;
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, accountId.ToString())
        };

        foreach (var permission in await AuthorizeBusiness.GetPermissions(accountId))
        {
            claims.Add(new Claim("permission", permission.ToString()));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = JwtOptions.Issuer,
            Audience = JwtOptions.Audience,
            Expires = DateTime.UtcNow.AddMinutes(JwtOptions.Lifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SigningKey)), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }


    public async Task<string> IsValidAuthenticate(AccountLoginInfo request)
    {
        if (request.password.Length < 8)
        {
            return "Password should have 8 letters atleast";
        }

        return string.Empty;
    }

}
