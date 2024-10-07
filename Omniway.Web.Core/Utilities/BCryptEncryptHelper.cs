using Omniway.Web.Core.Interfaces;
using BlueCat = BCrypt.Net.BCrypt;

namespace Omniway.Web.Core.Utilities;

internal class BCryptEncryptHelper : IEncryptHelper
{
    public string Encrypt(string value)
    {
        return BlueCat.HashPassword(value, BlueCat.GenerateSalt());
    }

    public bool Verify(string source, string hashedText)
    {
        return BlueCat.Verify(source, hashedText);
    }
}