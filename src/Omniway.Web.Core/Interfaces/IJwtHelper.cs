using Omniway.Web.Core.Models;

namespace Omniway.Web.Core.Interfaces;

public interface IJwtHelper
{
    TokenModel GenerateToken(string userName, int expireSeconds);
}