using Practice.Web.Core.Models;

namespace Practice.Web.Core.Interfaces;

public interface IJwtHelper
{
    TokenModel GenerateToken(string userName, int expireSeconds);
}