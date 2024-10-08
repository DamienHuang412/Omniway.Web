using Microsoft.EntityFrameworkCore;
using Practice.Web.Core.Entities;
using Practice.Web.Core.Interfaces;

namespace Practice.Web.Core.Services;

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