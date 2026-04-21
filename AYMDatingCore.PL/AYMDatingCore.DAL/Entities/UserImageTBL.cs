using AYMDatingCore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDatingCore.DAL.Entities
{
    public class UserImageTBL : BaseEntity<int>
    {
        public string? AppUserId { get; set; }
        public string? ImageUrl { get; set; } = null!;
        public virtual AppUser? AppUser { get; set; }
    }
}
