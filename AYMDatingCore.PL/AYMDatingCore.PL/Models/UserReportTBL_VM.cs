using AYMDatingCore.DAL.BaseEntity;

namespace AYMDatingCore.PL.Models
{
    public class UserReportTBL_VM : BaseEntity<int>
    {
        public string? SenderAppUserId { get; set; }
        public string? ReceiverAppUserId { get; set; }
        public string? Complaint { get; set; } = null!;
        public virtual AppUser? SenderAppUser { get; set; }
        public virtual AppUser? ReceiverAppUser { get; set; }

    }
}
