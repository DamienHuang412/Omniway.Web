using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Omniway.Web.Core;

internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OmniwayDbContext>
{
    public OmniwayDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<OmniwayDbContext>();

        builder.UseSqlite("Data Source=omniway.sqlite");

        return new OmniwayDbContext(builder.Options);
    }
}