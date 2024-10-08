using Practice.Web.Core.Models;

namespace Practice.Web.Core.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticateModel> Login(string userName, string password, CancellationToken cancellationToken);
}