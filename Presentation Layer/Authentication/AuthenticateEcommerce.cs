using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using Options;
using DTOs;
using Business_Layer.Business;
using Bussiness_Layer.Interfaces;

namespace Presentation_Layer.Authentication;

public class AuthenticateEcommerce
{
    public JwtOptions JwtOptions { get; }
    public IAuthorizeBusiness AuthorizeBusiness { get; }

    public AuthenticateEcommerce(JwtOptions jwtOptions, IAuthorizeBusiness authorizeBusiness)
    {
        JwtOptions = jwtOptions;
        AuthorizeBusiness = authorizeBusiness;
    }


    public async Task<string> CreateToken(int accountId)
    {
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




}
