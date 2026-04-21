using AYMDatingCore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDatingCore.DAL.Entities
{
    public class AdminPanelTBL : BaseEntity<int>
    {
        public string PanelName { get; set; } = string.Empty;
        public bool Activation { get; set; } = false;
        public int? UserViewsCounter { get; set; } = 0;
    }
}
