using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Practice.Web.Core.Interfaces;
using Practice.Web.Core.Models;

namespace Practice.Web.Core.Utilities;

internal class JwtHelper : IJwtHelper
{
    private readonly string _issuer;
    private readonly string _signKey;

    public JwtHelper(IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings")
            .GetChildren()
            .ToDictionary(x => x.Key, x => x.Value ?? string.Empty);

        if (!jwtSettings.TryGetValue("Issuer", out _issuer!)  || 
            !jwtSettings.TryGetValue("SigningKey", out _signKey!))
        {
            throw new ArgumentException("JWT settings are missing");
        }
    }

    public TokenModel GenerateToken(string userName, int expireSeconds)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, userName),
            new(JwtRegisteredClaimNames.Sub, userName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, "Users")
        };
        
        var userClaimsIdentity = new ClaimsIdentity(claims, "Bearer");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _issuer,
            Subject = userClaimsIdentity,
            Expires = DateTime.UtcNow.AddSeconds(expireSeconds),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JsonWebTokenHandler();
        var serializeToken = tokenHandler.CreateToken(tokenDescriptor);

        return new TokenModel
        {
            Token = serializeToken,
            ClaimsIdentity = userClaimsIdentity
        };
    }
}