using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.AuthAPI.Data.Entities;

namespace ProjectHive.Services.AuthAPI.Data;

public class ProjectHiveAuthDbContext(DbContextOptions<ProjectHiveAuthDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserFriend> Friends { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFriend>()
            .HasKey(f => new { f.UserAId, f.UserBId });

        modelBuilder.Entity<UserFriend>()
            .HasOne(f => f.UserA)
            .WithMany(u => u.Friends)
            .HasForeignKey(f => f.UserAId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<UserFriend>()
            .HasOne(f => f.UserB)
            .WithMany()
            .HasForeignKey(f => f.UserBId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
