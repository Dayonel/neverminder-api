using System.ComponentModel.DataAnnotations;

namespace Neverminder.Core.DTO
{
    public class ReminderDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime AlertOn { get; set; }
        public DateTime? SentAt { get; set; }
        public bool Enabled { get; set; }
        public bool Sent { get; set; }
    }
}
