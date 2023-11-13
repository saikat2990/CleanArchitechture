using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CleanArchitechture.Repositories;

public sealed class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    private readonly ICurrentUserService _currentUserService;

    public AppDbContext(DbContextOptions<AppDbContext> opt, ICurrentUserService currentUserService) : base(opt)
    {

    }
      
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUser>().ToTable("Users");
        modelBuilder.Entity<AppRole>().ToTable("Roles");
    }
}