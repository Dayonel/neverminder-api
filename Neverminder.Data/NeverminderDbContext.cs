using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Neverminder.Core.Entity;

namespace Neverminder.Data
{
    public sealed class NeverminderDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Platform> Platforms { get; set; }

        public NeverminderDbContext(DbContextOptions options) : base(options) { }
    }
}
