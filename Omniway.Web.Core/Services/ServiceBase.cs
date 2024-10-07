using Microsoft.EntityFrameworkCore;
using Omniway.Web.Core.Interfaces;

namespace Omniway.Web.Core.Services;

internal abstract class ServiceBase
{
    protected readonly IDbContextFactory<OmniwayDbContext> _dbContextFactory;
    protected readonly IEncryptHelper _encryptHelper;
    
    internal ServiceBase(IDbContextFactory<OmniwayDbContext> dbContextFactory, IEncryptHelper encryptHelper)
    {
        _dbContextFactory = dbContextFactory;
        _encryptHelper = encryptHelper;
    }
}