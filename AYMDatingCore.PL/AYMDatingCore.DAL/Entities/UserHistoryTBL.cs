using AYMDatingCore.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDatingCore.DAL.Entities
{
    public class UserHistoryTBL : BaseEntity<int>
    {
        public string? AppUserId { get; set; }
        public int? CountryId { get; set; }
        public int? LanguageId { get; set; }
        public int? GenderId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? ProfessionId { get; set; }
        public int? PurposeId { get; set; }
        public int? FinancialModeId { get; set; }
        public int? EducationId { get; set; }

        public string? MainImageUrl { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ProfileHeading { get; set; } = null!;
        public string AboutYou { get; set; } = null!;
        public string AboutPartner { get; set; } = null!;
        public bool IsMain { get; set; } = true;
        public bool IsSwitchedOff { get; set; } = false;

        public int? SearchAgeFrom { get; set; }
        public int? SearchAgeTo { get; set; }

        public virtual AppUser? AppUser { get; set; }
        public virtual CountryTBL? Country { get; set; }
        public virtual LanguageTBL? Language { get; set; }
        public virtual GenderTBL? Gender { get; set; }
        public virtual MaritalStatusTBL? MaritalStatus { get; set; }
        public virtual ProfessionTBL? Profession { get; set; }
        public virtual PurposeTBL? Purpose { get; set; }
        public virtual FinancialModeTBL? FinancialMode { get; set; }
        public virtual EducationTBL? Education { get; set; }
    }
}
