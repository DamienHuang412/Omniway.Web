using Omniway.Web.Core.Models;

namespace Omniway.Web.Core.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticateModel> Login(string username, string password, CancellationToken cancellationToken);
}