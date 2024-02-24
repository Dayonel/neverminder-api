using Neverminder.Core.Entity;
using Neverminder.Core.Interfaces.Repositories.Base;

namespace Neverminder.Core.Interfaces.Repositories
{
    public interface IReminderRepository : IRepository<Reminder>
    {
        Task<List<Reminder>> ListPushNotifications(int page, int pageSize);
        Task<bool> MarkSent(List<Reminder> reminders);
    }
}
