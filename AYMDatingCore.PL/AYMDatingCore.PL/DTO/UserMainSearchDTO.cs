using Microsoft.AspNetCore.Mvc.Rendering;

namespace AYMDatingCore.PL.DTO
{
    public class UserMainSearchDTO
    {
        //public int? IamGender { get; set; }
        public int? IwantGender { get; set; }
        public int? CountrySearch { get; set;}
        public int? AgeFrom { get; set;}
        public string? City { get; set;}
        public int? AgeTo { get; set;}
        public bool UserHasImage { get; set; }

        public List<SelectListItem>? Countries { get; set; }
        public List<SelectListItem>? Genders { get; set; }


    }
}
