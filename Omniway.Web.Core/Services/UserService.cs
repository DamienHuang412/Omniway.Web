using Omniway.Web.Core.Entities;
using Omniway.Web.Core.Interfaces;
using Omniway.Web.Core.Models;

namespace Omniway.Web.Core.Services;

internal class UserService(IUserRepository userRepository, IEncryptHelper encryptHelper)
    : ServiceBase(userRepository, encryptHelper), IUserService, IHealthCheckable
{
    public async Task<bool> Check(CancellationToken cancellationToken)
    {
        var adminUser = await _userRepository.GetByName(HardCode.DefaultUserName, cancellationToken);
        
        return adminUser != null;
    }

    public async Task<UserModel> Create(RegisterModel model, CancellationToken cancellationToken)
    {
        var userId = await _userRepository.Create(new UserEntity
        {
            UserName = model.UserName,
            Password = _encryptHelper.Encrypt(model.RawPassword)
        }, cancellationToken);

        return new UserModel
        {
            Id = userId,
            UserName = model.UserName
        };
    }

    public async Task<UserModel> ChangePassword(ChangePasswordModel model, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.Login(model.UserName, model.OldPassword, cancellationToken);

        if (userEntity == null)
        {
            throw new ArgumentException("Invalid Old Password");
        }

        await _userRepository.ChangePassword(new UserEntity
        {
            Id = userEntity.Id,
            Password = model.NewPassword
        }, cancellationToken);

        return new UserModel
        {
            Id = userEntity.Id,
            UserName = model.UserName
        };
    }
}