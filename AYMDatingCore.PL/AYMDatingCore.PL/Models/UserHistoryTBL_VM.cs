using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AYMDatingCore.PL.Models
{
    public class UserHistoryTBL_VM : BaseEntity<int>
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

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        [MaxLength(50, ErrorMessage = "City must be at max 50 character")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Heading is required")]
        [Display(Name = "Heading")]
        [MaxLength(100, ErrorMessage = "Profile Heading must be at max 100 character")]
        public string ProfileHeading { get; set; } = null!;

        [Required(ErrorMessage = "About You is required")]
        [Display(Name = "About You")]
        [MaxLength(1000)]
        public string AboutYou { get; set; } = null!;

        [Required(ErrorMessage = "About Partner is required")]
        [Display(Name = "About Partner")]
        [MaxLength(1000)]
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

        public List<UserImageTBL_VM> UserImageTBL_VM { get; set; } = new List<UserImageTBL_VM>();
        public bool IsLiked { get; set; } = false;
        public bool Isblocked { get; set; } = false;
        public bool IsFavorite { get; set; } = false;

        public IEnumerable<SelectListItem> EducationOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ProfessionOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> MaritalStatusOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> LanguageOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> PurposeOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> FinancialModeOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> CountryOptions { get; set; } = new List<SelectListItem>();

    }
}
