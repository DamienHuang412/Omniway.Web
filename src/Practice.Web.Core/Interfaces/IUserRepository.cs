using Practice.Web.Core.Entities;
using Practice.Web.Core.Models;

namespace Practice.Web.Core.Interfaces;

internal interface IUserRepository
{
    Task<int> Create(UserEntity user, CancellationToken cancellationToken);
    
    Task<UserPaginationModel> Read(int pageIndex, int pageSize, CancellationToken cancellationToken);

    Task<UserEntity?> GetByName(string userName, CancellationToken cancellationToken);
    
    Task ChangePassword(UserEntity user, CancellationToken cancellationToken);
}