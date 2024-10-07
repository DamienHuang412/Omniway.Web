using Microsoft.EntityFrameworkCore;
using Omniway.Web.Core.Entities;
using Omniway.Web.Core.Interfaces;

namespace Omniway.Web.Core.Services;

internal class DataInitialService(IDbContextFactory<OmniwayDbContext> dbContextFactory, IEncryptHelper encryptHelper)
    : ServiceBase(dbContextFactory, encryptHelper), IDataInitialService
{
    private const string DefaultUserName = "admin";
    private const string DefaultUserPassword = "pass.123";

    public async Task Initialize(CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var adminUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == DefaultUserName, cancellationToken);

        if (adminUser == null)
        {
            await dbContext.Users.AddAsync(new UserEntity
            {
                UserName = DefaultUserName,
                Password = _encryptHelper.Encrypt(DefaultUserPassword, DefaultUserName)
            }, cancellationToken);
        }
    }
}