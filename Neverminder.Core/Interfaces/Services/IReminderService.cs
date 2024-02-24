using Neverminder.Core.DTO;

namespace Neverminder.Core.Interfaces.Services
{
    public interface IReminderService
    {
        Task<bool> AddReminder(string pushToken, ReminderDTO model);
        Task<ReminderDTO> GetReminder(string pushToken, int id);
        Task<List<ReminderDTO>> ListReminders(string pushToken, int page, int pageSize);
        Task<bool> UpdateReminder(string pushToken, int id, ReminderDTO model);
        Task<bool> DeleteReminder(string pushToken, int id);
    }
}
