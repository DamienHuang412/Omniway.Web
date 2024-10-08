namespace Omniway.Web.Core.Interfaces;

public interface IEncryptHelper
{
    string Encrypt(string value);

    bool Verify(string source, string hashedText);
}