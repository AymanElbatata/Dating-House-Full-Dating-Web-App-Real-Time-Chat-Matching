using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Entities;

namespace AYMDatingCore.PL.Models
{
    public class ProfessionTBL_VM : BaseEntity<int>
    {
        public string? Name { get; set; } = null!;
    }
}
