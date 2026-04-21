using System.ComponentModel.DataAnnotations;

namespace AYMDatingCore.PL.DTO
{
    public class UserReport_DTO
    {
        public string? ReceiverAppUserUsername { get; set; }

        [MinLength(5, ErrorMessage = "Minimum Length 5 Charcters")]
        [MaxLength(1000, ErrorMessage = "Maximum Length 1000 Charcter.")]
        [Display(Name = "Your Complaint")]
        [Required(ErrorMessage = "Complaint is required.")]
        public string? Complaint { get; set; }
    }
}
