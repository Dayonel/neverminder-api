using Neverminder.Core.Entity;
using Neverminder.Core.Interfaces.Repositories;
using Neverminder.Data.Repositories.Base;

namespace Neverminder.Data.Repositories
{
    public class ReminderRepository : BaseRepository<Reminder>, IReminderRepository
    {
        public ReminderRepository(NeverminderDbContext dbContext) : base(dbContext) { }
    }
}
