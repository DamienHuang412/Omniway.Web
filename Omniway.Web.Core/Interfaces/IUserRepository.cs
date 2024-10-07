using Omniway.Web.Core.Entities;

namespace Omniway.Web.Core.Interfaces;

internal interface IUserRepository
{
    Task<int> Create(UserEntity user, CancellationToken cancellationToken);
    
    Task<UserEntity[]> ReadAll(CancellationToken cancellationToken);

    Task<UserEntity?> GetByName(string userName, CancellationToken cancellationToken);
    
    Task<UserEntity?> Login(string username, string password, CancellationToken cancellationToken);
    
    Task Update(UserEntity user, CancellationToken cancellationToken);
}