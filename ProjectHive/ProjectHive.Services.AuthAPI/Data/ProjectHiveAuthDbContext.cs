using Microsoft.EntityFrameworkCore;
using ProjectHive.Services.AuthAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectHive.Services.AuthAPI.Data
{
    public class ProjectHiveAuthDbContext:DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        public ProjectHiveAuthDbContext(DbContextOptions<ProjectHiveAuthDbContext> options) : base(options) { }

    }
}
