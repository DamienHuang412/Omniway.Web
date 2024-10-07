namespace Omniway.Web.Core.Interfaces;

public interface IJwtHelper
{ 
    string Issuer { get; }
    
    string SigningKey { get; }
    
    string GenerateToken(string userName, int expireSeconds);
}