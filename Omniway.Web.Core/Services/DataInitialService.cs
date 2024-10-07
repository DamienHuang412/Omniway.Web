using Microsoft.EntityFrameworkCore;
using Omniway.Web.Core.Entities;
using Omniway.Web.Core.Interfaces;

namespace Omniway.Web.Core.Services;

internal class DataInitialService(IUserRepository userRepository, IEncryptHelper encryptHelper)
    : ServiceBase(userRepository, encryptHelper), IDataInitialService
{
    private const string DefaultUserPassword = "pass.123";

    public async Task Initialize(CancellationToken cancellationToken)
    {
        var adminUser = await _userRepository.GetByName(HardCode.DefaultUserName, cancellationToken);

        if (adminUser == null)
        {
            await _userRepository.Create(new UserEntity
            {
                UserName = HardCode.DefaultUserName,
                Password = _encryptHelper.Encrypt(DefaultUserPassword)
            }, cancellationToken);
        }
    }
}