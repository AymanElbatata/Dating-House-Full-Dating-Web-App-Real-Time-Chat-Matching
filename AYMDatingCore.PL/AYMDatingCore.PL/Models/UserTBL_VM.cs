using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Entities;

namespace AYMDatingCore.PL.Models
{
    public class UserTBL_VM : BaseEntity<string>
    {
        public int? CountryTBLId { get; set; }
        public int? GenderTBLId { get; set; }
        public int? ProfessionTBLId { get; set; }

        public virtual CountryTBL? CountryTBL { get; set; }
        public virtual GenderTBL? GenderTBL { get; set; }
        public virtual ProfessionTBL? ProfessionTBL { get; set; }


        public string? Email { get; set; } = null!;
        public string? Role { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? UserName { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; } = null!;

    }
}
