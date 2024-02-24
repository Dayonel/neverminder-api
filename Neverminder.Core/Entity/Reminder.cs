﻿using Neverminder.Core.Entity.Base;

namespace Neverminder.Core.Entity
{
    public class Reminder : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime AlertOn { get; set; }
        public bool Enabled { get; set; }

        public virtual Platform Platform { get; set; }
        public int PlatformId { get; set; }
    }
}
