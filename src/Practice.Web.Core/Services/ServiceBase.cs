using Microsoft.EntityFrameworkCore;
using Practice.Web.Core.Interfaces;

namespace Practice.Web.Core.Services;

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