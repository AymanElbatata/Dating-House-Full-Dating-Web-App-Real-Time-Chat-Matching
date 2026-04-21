using AYMDatingCore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDatingCore.DAL.Entities
{
    public class UserMessageGroupTBL : BaseEntity<int>
    {
        public string NameGuid { get; set; } = null!;
        public string? SenderAppUserId { get; set; }
        public string? ReceiverAppUserId { get; set; }

        public virtual AppUser? SenderAppUser { get; set; }
        public virtual AppUser? ReceiverAppUser { get; set; }
    }
}
