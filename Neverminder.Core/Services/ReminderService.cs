using Neverminder.Core.DTO;
using Neverminder.Core.Interfaces.Repositories;
using Neverminder.Core.Interfaces.Services;
using Neverminder.Core.Mappers;

namespace Neverminder.Core.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IReminderRepository _reminderRepository;
        public ReminderService(IPlatformRepository platformRepository,
            IReminderRepository reminderRepository)
        {
            _platformRepository = platformRepository;
            _reminderRepository = reminderRepository;
        }

        public async Task<bool> AddReminder(string pushToken, ReminderDTO model)
        {
            var platform = await _platformRepository.Get(f => f.PushToken == pushToken);
            if (platform == null)
            {
                return false;
            }

            var entity = model.Map(platform.Id);
            return await _reminderRepository.AddAsync(entity) > 0;
        }

        public async Task<ReminderDTO> GetReminder(string pushToken, int id)
        {
            var platform = await _platformRepository.Get(f => f.PushToken == pushToken);
            if (platform == null)
            {
                return null;
            }

            return (await _reminderRepository.Get(f => f.Id == id && f.PlatformId == platform.Id)).Map();
        }

        public async Task<bool> UpdateReminder(string pushToken, int id, ReminderDTO model)
        {
            var platform = await _platformRepository.Get(f => f.PushToken == pushToken);
            if (platform == null)
            {
                return false;
            }

            var reminder = await _reminderRepository.Get(f => f.Id == id && f.PlatformId == platform.Id);
            if (reminder == null)
            {
                return false;
            }

            reminder.Title = model.Title;
            reminder.Description = model.Description;
            reminder.AlertOn = model.AlertOn;
            reminder.Enabled = model.Enabled;

            return await _reminderRepository.UpdateAsync(reminder);
        }

        public async Task<bool> DeleteReminder(string pushToken, int id)
        {
            var platform = await _platformRepository.Get(f => f.PushToken == pushToken);
            if (platform == null)
            {
                return false;
            }

            var reminder = await _reminderRepository.AnyAsync(f => f.Id == id && f.PlatformId == platform.Id);
            if (!reminder)
            {
                return false;
            }

            return await _reminderRepository.DeleteAsync(id);
        }

        public async Task<List<ReminderDTO>> ListReminders(string pushToken, int page, int pageSize)
        {
            var result = new List<ReminderDTO>();

            var platform = await _platformRepository.Get(f => f.PushToken == pushToken);
            if (platform == null)
            {
                return null;
            }

            var items = await _reminderRepository.ListPaged(page, pageSize);
            items.ForEach(f =>
            {
                result.Add(f.Map());
            });

            return result;
        }
    }
}
