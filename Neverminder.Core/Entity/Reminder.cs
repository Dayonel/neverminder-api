using Neverminder.Core.Entity.Base;

namespace Neverminder.Core.Entity
{
    public class Reminder : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime AlertOn { get; set; }
        public DateTime? SentAt { get; set; }
        public bool Enabled { get; set; }
        public bool Sent { get; set; }

        public virtual Platform Platform { get; set; }
        public int PlatformId { get; set; }
    }
}
