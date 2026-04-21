using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Entities;

namespace AYMDatingCore.PL.Models
{
    public class Chat_VM
    {
        public string? SenderAppUserId { get; set; }
        public string? ReceiverAppUserId { get; set; }
        public virtual AppUser? SenderAppUser { get; set; }
        public virtual AppUser? ReceiverAppUser { get; set; }


        public string? GroupName { get; set; }
        public bool IsThereBlocking { get; set; } = false;

        public List<UserMessage_VM> UserMessage_VM { get; set; } = new List<UserMessage_VM>();

        public UserHistoryTBL_VM UserHistoryTBL_VM { get; set; } = new UserHistoryTBL_VM();

        public List<UserChat_VM> Users_VM { get; set; } = new List<UserChat_VM>();

    }
}
