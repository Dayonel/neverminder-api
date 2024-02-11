using Neverminder.Core.Entity;
using Neverminder.Core.Interfaces.Repositories;
using Neverminder.Data.Repositories.Base;

namespace Neverminder.Data.Repositories
{
    public class PlatformRepository : BaseRepository<Platform>, IPlatformRepository
    {
        public PlatformRepository(NeverminderDbContext dbContext) : base(dbContext) { }
    }
}
