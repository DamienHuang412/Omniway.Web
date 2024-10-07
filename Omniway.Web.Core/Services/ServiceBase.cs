using Microsoft.EntityFrameworkCore;
using Omniway.Web.Core.Interfaces;

namespace Omniway.Web.Core.Services;

internal abstract class ServiceBase
{
    protected readonly IUserRepository _userRepository;
    protected readonly IEncryptHelper _encryptHelper;
    
    internal ServiceBase(IUserRepository userRepository, IEncryptHelper encryptHelper)
    {
        _userRepository = userRepository;
        _encryptHelper = encryptHelper;
    }
}