using AYMDatingCore.DAL.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace AYMDatingCore.PL.Models
{
    public class UserMessage_VM : BaseEntity<int>
    {
        public string? SenderAppUserId { get; set; }
        public string? ReceiverAppUserId { get; set; }
        public string? Message { get; set; } = null!;
        public string? AudioDataUrl { get; set; } = null!;
        public string? FileDataUrl { get; set; } = null!;

        public bool IsSeen { get; set; } = false;
        public bool IsDeletedFromSender { get; set; } = false;
        public bool IsDeletedFromReceiver { get; set; } = false;

        public virtual AppUser? SenderAppUser { get; set; }
        public virtual AppUser? ReceiverAppUser { get; set; }
    }
}
