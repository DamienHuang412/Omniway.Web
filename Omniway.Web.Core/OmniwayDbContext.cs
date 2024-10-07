using Microsoft.EntityFrameworkCore;
using Omniway.Web.Core.Entities;

namespace Omniway.Web.Core;

internal class OmniwayDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public OmniwayDbContext(DbContextOptions<OmniwayDbContext> options) : base(options)
    {
    }

}