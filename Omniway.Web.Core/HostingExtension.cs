using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Omniway.Web.Core.Interfaces;
using Omniway.Web.Core.Services;
using Omniway.Web.Core.Utilities;

namespace Omniway.Web.Core;

public static class HostingExtension
{
    public static IServiceCollection UseCore(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        builder.Services.AddSingleton<IDataInitialService, DataInitialService>();
        builder.Services.AddSingleton<IEncryptHelper, BCryptEncryptHelper>();
        
        return builder.Services.AddDbContextFactory<OmniwayDbContext>(
            options => options.UseSqlite(configuration.GetConnectionString("SqlLite")));
    }
}