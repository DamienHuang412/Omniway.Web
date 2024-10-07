using Omniway.Web.Core.Interfaces;
using BlueCat = BCrypt.Net.BCrypt;

namespace Omniway.Web.Core.Utilities;

internal class BCryptEncryptHelper : IEncryptHelper
{
    private const string Salt = "!@#$%^&*()";
    
    public string Encrypt(string value)
    {
        return BlueCat.HashPassword(value, Salt);
    }
}