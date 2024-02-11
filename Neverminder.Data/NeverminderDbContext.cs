using Microsoft.EntityFrameworkCore;
using Neverminder.Core.Entity;

namespace Neverminder.Data
{
    public sealed class NeverminderDbContext : DbContext
    {
        public DbSet<Platform> Platforms { get; set; }

        public NeverminderDbContext(DbContextOptions options) : base(options) { }
    }
}
