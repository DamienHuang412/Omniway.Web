using Microsoft.EntityFrameworkCore;
using Omniway.Web.Core.Entities;

namespace Omniway.Web.Core;

internal class OmniwayDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public OmniwayDbContext(DbContextOptions<OmniwayDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(eb =>
        {
            eb.HasKey(u => u.Id);
            eb.Property(u => u.Id).ValueGeneratedOnAdd();
            eb.Property(u => u.UserName)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();
            eb.HasIndex(u => u.UserName).IsUnique();
            eb.Property(u => u.Password)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();
        });
    }
}