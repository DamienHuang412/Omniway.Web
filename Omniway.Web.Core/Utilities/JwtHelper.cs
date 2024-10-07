using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Omniway.Web.Core.Interfaces;

namespace Omniway.Web.Core.Utilities;

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
    
    public string Issuer => _issuer;
    
    public string SigningKey => _signKey;

    public string GenerateToken(string userName, int expireSeconds)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Name, userName),
            new(JwtRegisteredClaimNames.Sub, userName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, "Users")
        };

        var userClaimsIdentity = new ClaimsIdentity(claims);

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

        return serializeToken;
    }
}