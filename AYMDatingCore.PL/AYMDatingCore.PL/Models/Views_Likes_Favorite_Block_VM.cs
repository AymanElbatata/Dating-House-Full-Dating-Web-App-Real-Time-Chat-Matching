using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Entities;
using AYMDatingCore.PL.DTO;

namespace AYMDatingCore.PL.Models
{
    public class Views_Likes_Favorite_Block_VM
    {

        public List<UserHistoryTBL_VM> UserHistoryTBL_VM { get; set; } = new List<UserHistoryTBL_VM>();

    }
}
