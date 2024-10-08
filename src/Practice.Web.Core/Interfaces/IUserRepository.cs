using Practice.Web.Core.Entities;

namespace Practice.Web.Core.Interfaces;

internal interface IUserRepository
{
    Task<int> Create(UserEntity user, CancellationToken cancellationToken);
    
    Task<UserEntity[]> ReadAll(CancellationToken cancellationToken);

    Task<UserEntity?> GetByName(string userName, CancellationToken cancellationToken);
    
    Task ChangePassword(UserEntity user, CancellationToken cancellationToken);
}