using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Entities;

namespace AYMDatingCore.PL.Models
{
    public class UserImageTBL_VM : BaseEntity<int>
    {
        public string? AppUserId { get; set; }
        public string? ImageUrl { get; set; } = null!;
        public virtual AppUser? AppUser { get; set; }
    }
}
