using Omniway.Web.Core.Interfaces;
using BlueCat = BCrypt.Net.BCrypt;

namespace Omniway.Web.Core.Utilities;

internal class BCryptEncryptHelper : IEncryptHelper
{
    public string Encrypt(string value, string salt)
    {
        return BlueCat.HashPassword(value, salt);
    }
}