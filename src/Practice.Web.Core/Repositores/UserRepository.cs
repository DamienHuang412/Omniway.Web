using Microsoft.EntityFrameworkCore;
using Practice.Web.Core.Entities;
using Practice.Web.Core.Interfaces;
using Practice.Web.Core.Models;

namespace Practice.Web.Core.Repositores;

internal class UserRepository(IDbContextFactory<PracticeDbContext> dbContextFactory) : IUserRepository
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

    public async Task<UserPaginationModel> Read(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var totalCount = await context.Users.CountAsync(cancellationToken);
        var users = context.Users
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new UserModel
            {
                Id = x.Id,
                UserName = x.UserName
            }).ToArray();

        return new UserPaginationModel
        {
            Data = users,
            TotalCount = totalCount
        };
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