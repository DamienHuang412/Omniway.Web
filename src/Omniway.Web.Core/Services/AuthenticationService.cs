using Omniway.Web.Core.Interfaces;
using Omniway.Web.Core.Models;

namespace Omniway.Web.Core.Services;

internal class AuthenticationService(
    IUserRepository userRepository,
    IEncryptHelper encryptHelper,
    IJwtHelper jwtHelper)
    : ServiceBase(userRepository, encryptHelper), IAuthenticationService
{
    public async Task<AuthenticateModel> Login(string userName, string password, CancellationToken cancellationToken)
    {
        var loginResult = new AuthenticateModel();
        var user = await _userRepository.GetByName(userName, cancellationToken);

        if (user == null || !_encryptHelper.Verify(password, user.Password)) return loginResult;
        
        loginResult.IsSuccess = true;
        loginResult.Token = jwtHelper.GenerateToken(user.UserName, 10);

        return loginResult;
    }
}