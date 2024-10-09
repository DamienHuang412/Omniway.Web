using Microsoft.EntityFrameworkCore;
using Practice.Web.Core.Entities;

namespace Practice.Web.Core;

internal class PracticeDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public PracticeDbContext(DbContextOptions<PracticeDbContext> options) : base(options)
    {
        Database.EnsureCreated();
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