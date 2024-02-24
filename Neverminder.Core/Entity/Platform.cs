using Neverminder.Core.Entity.Base;

namespace Neverminder.Core.Entity
{
    public class Platform : EntityBase
    {
        public string PushToken { get; set; }

        public virtual List<Reminder> Reminders { get; set; }
    }
}
