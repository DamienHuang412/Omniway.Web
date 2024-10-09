using Practice.Web.Core.Models;

namespace Practice.Web.Core.Interfaces;

public interface IUserService
{
    Task<UserModel> Create(RegisterModel model, CancellationToken cancellationToken);
    
    Task<UserModel> ChangePassword(ChangePasswordModel model, CancellationToken cancellationToken);

    Task<UserPaginationModel> Read(int pageIndex, int pageSize, CancellationToken cancellationToken);
}