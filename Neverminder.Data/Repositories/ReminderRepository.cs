using Microsoft.EntityFrameworkCore;
using Neverminder.Core.Entity;
using Neverminder.Core.Interfaces.Repositories;
using Neverminder.Data.Repositories.Base;

namespace Neverminder.Data.Repositories
{
    public class ReminderRepository : BaseRepository<Reminder>, IReminderRepository
    {
        private readonly NeverminderDbContext _dbContext;
        public ReminderRepository(NeverminderDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Reminder>> ListPushNotifications(int page, int pageSize)
        {
            return await _dbContext.Reminders
                .Include(i => i.Platform)
                .Where(w => !w.Sent 
                && w.Enabled
                && w.AlertOn >= DateTime.UtcNow.AddMinutes(-10)
                && w.AlertOn <= DateTime.UtcNow)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .OrderBy(o => o.Id)
                .ToListAsync();
        }

        public async Task<bool> MarkSent(List<Reminder> reminders)
        {
            reminders.ForEach(f =>
            {
                f.Sent = true;
                f.SentAt = DateTime.UtcNow;
            });

            _dbContext.UpdateRange(reminders);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
