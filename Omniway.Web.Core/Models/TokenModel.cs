using System.Security.Claims;

namespace Omniway.Web.Core.Models;

public class TokenModel
{
    public ClaimsIdentity ClaimsIdentity { get; set; }

    public string Token { get; set; }
}