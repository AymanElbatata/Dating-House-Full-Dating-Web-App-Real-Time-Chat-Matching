using AYMDatingCore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDatingCore.DAL.Entities
{
    public class UserAddressListTBL : BaseEntity<int>
    {
        public string? AppUserId { get; set; }
        public string? IpAddress { get; set; } = null!;
        public string? HostName { get; set; } = null!;
        public string? Browser { get; set; } = null!;
        public string? OperationType { get; set; } = null!;
        public virtual AppUser? AppUser { get; set; }
    }
}
