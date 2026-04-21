using AYMDatingCore.DAL.BaseEntity;

namespace AYMDatingCore.PL.Models
{
    public class UserAddressListTBL_VM : BaseEntity<int>
    {
        public string? AppUserId { get; set; }
        public string? IpAddress { get; set; } = null!;
        public string? HostName { get; set; } = null!;
        public string? Browser { get; set; } = null!;
        public string? OperationType { get; set; } = null!;
        public virtual AppUser? AppUser { get; set; }
    }
}
