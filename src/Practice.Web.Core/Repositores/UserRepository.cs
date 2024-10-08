using Microsoft.EntityFrameworkCore;
using Practice.Web.Core.Entities;
using Practice.Web.Core.Interfaces;

namespace Practice.Web.Core.Repositores;

internal class UserRepository(IDbContextFactory<OmniwayDbContext> dbContextFactory) : IUserRepository
{
    public async Task<int> Create(UserEntity user, CancellationToken cancellationToken)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var existsUser = context.Users.FirstOrDefault(x => x.UserName == user.UserName);
        
        if(existsUser != null) return existsUser.Id;
        
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

    public async Task ChangePassword(UserEntity user, CancellationToken cancellationToken)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var entity = context.Users.FirstOrDefault(u => u.Id == user.Id);

        if (entity != null)
        {
            entity.Password = user.Password;

            context.Users.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}