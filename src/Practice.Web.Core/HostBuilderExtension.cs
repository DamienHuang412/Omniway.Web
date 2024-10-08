using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Practice.Web.Core.Interfaces;
using Practice.Web.Core.Repositores;
using Practice.Web.Core.Services;
using Practice.Web.Core.Utilities;

namespace Practice.Web.Core;

public static class HostBuilderExtension
{
    public static IServiceCollection UseCore(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var serviceCollection = builder.Services;
        
        serviceCollection.AddSingleton<IUserRepository, UserRepository>();
        
        serviceCollection.AddSingleton<IDataInitialService, DataInitialService>();
        serviceCollection.AddSingleton<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddSingleton<IUserService, UserService>();
        serviceCollection.AddSingleton<IHealthCheckable, UserService>();
        
        serviceCollection.AddSingleton<IEncryptHelper, BCryptEncryptHelper>();
        serviceCollection.AddSingleton<IJwtHelper, JwtHelper>();
        
        return serviceCollection.AddDbContextFactory<OmniwayDbContext>(
            options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
    }
}