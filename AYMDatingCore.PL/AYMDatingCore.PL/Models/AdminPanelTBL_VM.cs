using AYMDatingCore.DAL.BaseEntity;

namespace AYMDatingCore.PL.Models
{
    public class AdminPanelTBL_VM : BaseEntity<int>
    {
        public string PanelName { get; set; } = string.Empty;
        public bool Activation { get; set; } = false;
    }
}
