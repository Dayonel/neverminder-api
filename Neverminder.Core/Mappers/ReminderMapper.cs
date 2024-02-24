using Neverminder.Core.DTO;
using Neverminder.Core.Entity;

namespace Neverminder.Core.Mappers
{
    public static class ReminderMapper
    {
        public static Reminder Map(this ReminderDTO model, int platformId)
        {
            return model != null
                ?
                new Reminder
                {
                    AlertOn = model.AlertOn,
                    CreatedOn = model.CreatedOn,
                    Description = model.Description,
                    Enabled = model.Enabled,
                    Title = model.Title,
                    PlatformId = platformId,
                }
                : null;
        }

        public static ReminderDTO Map(this Reminder model)
        {
            return model != null
                ?
                new ReminderDTO
                {
                    Id = model.Id,
                    AlertOn = model.AlertOn,
                    CreatedOn = model.CreatedOn,
                    Description = model.Description,
                    Enabled = model.Enabled,
                    Title = model.Title,
                }
                : null;
        }
    }
}
