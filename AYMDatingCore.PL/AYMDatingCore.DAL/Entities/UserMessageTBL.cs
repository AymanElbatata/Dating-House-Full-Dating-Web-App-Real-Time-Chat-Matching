using AYMDatingCore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDatingCore.DAL.Entities
{
    public class UserMessageTBL : BaseEntity<int>
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
