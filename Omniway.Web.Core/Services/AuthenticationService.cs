using Microsoft.EntityFrameworkCore;
using Omniway.Web.Core.Interfaces;
using Omniway.Web.Core.Models;

namespace Omniway.Web.Core.Services;

internal class AuthenticationService : ServiceBase, IAuthenticationService
{
    private readonly IJwtHelper _jwtHelper;
    
    public AuthenticationService(IUserRepository userRepository, 
        IEncryptHelper encryptHelper, 
        IJwtHelper jwtHelper) 
        : base(userRepository, encryptHelper)
    {
        _jwtHelper = jwtHelper;
    }

    public async Task<AuthenticateModel> Login(string userName, string password, CancellationToken cancellationToken)
    {
        var loginResult = new AuthenticateModel();
        var user = await _userRepository.GetByName(userName, cancellationToken);

        if (user == null || !_encryptHelper.Verify(password, user.Password)) return loginResult;
        
        loginResult.IsSuccess = true;
        loginResult.Token = _jwtHelper.GenerateToken(user.UserName, 10);

        return loginResult;
    }
}