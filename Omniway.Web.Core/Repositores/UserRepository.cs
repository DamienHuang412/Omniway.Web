using Microsoft.EntityFrameworkCore;
using Omniway.Web.Core.Entities;
using Omniway.Web.Core.Interfaces;

namespace Omniway.Web.Core.Repositores;

internal class UserRepository(IDbContextFactory<OmniwayDbContext> dbContextFactory) : IUserRepository
{
    public async Task<int> Create(UserEntity user, CancellationToken cancellationToken)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await context.Users.AddAsync(user, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Entity.Id;
    }

    public async Task<UserEntity[]> ReadAll(CancellationToken cancellationToken)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return context.Users.ToArray();
    }

    public async Task<UserEntity?> GetByName(string userName, CancellationToken cancellationToken)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        return context.Users.FirstOrDefault(u => u.UserName == userName);
    }

    public async Task<UserEntity?> Login(string username, string password, CancellationToken cancellationToken)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        return context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
    }

    public async Task Update(UserEntity user, CancellationToken cancellationToken)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var entity = context.Users.FirstOrDefault(u => u.UserName == user.UserName);

        if (entity != null)
        {
            entity.Password = user.Password;

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}