using Omniway.Web.Core.Models;

namespace Omniway.Web.Core.Interfaces;

public interface IUserService
{
    Task<UserModel> Create(RegisterModel model, CancellationToken cancellationToken);
    
    Task<UserModel> ChangePassword(ChangePasswordModel model, CancellationToken cancellationToken);
}