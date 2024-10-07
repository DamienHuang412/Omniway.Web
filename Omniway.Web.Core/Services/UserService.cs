using Microsoft.EntityFrameworkCore;
using Omniway.Web.Core.Interfaces;

namespace Omniway.Web.Core.Services;

internal class UserService(IDbContextFactory<OmniwayDbContext> dbContextFactory, IEncryptHelper encryptHelper)
    : ServiceBase(dbContextFactory, encryptHelper), IUserService
{
    
}