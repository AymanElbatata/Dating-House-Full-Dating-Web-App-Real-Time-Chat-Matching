using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AYMDatingCore.PL.Models
{
    public class UserSelectViewModel
    {
        [Required]
        public int UserId { get; set; }

        public List<SelectListItem> Users { get; set; }

    }
}
