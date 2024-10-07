namespace Omniway.Web.Core.Interfaces;

public interface IJwtHelper
{ 
    string GenerateToken(string userName, int expireSeconds);
}