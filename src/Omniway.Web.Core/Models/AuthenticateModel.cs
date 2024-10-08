namespace Omniway.Web.Core.Models;

public class AuthenticateModel
{
    public bool IsSuccess { get; set; }
    
    public TokenModel Token { get; set; }
}