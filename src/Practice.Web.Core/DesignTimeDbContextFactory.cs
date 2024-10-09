using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Practice.Web.Core;

internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PracticeDbContext>
{
    public PracticeDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<PracticeDbContext>();

        builder.UseSqlite("Data Source=omniway.sqlite");

        return new PracticeDbContext(builder.Options);
    }
}