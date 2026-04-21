using AYMDatingCore.DAL.BaseEntity;

namespace AYMDatingCore.PL.Models
{
    public class CountryTBL_VM : BaseEntity<int>
    {
        public string? Name { get; set; } = null!;
    }
}
