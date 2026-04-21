using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Entities;
using AYMDatingCore.PL.DTO;

namespace AYMDatingCore.PL.Models
{
    public class Messages_VM
    {

        public List<MessageCounterDTO> MessageCounterDTO { get; set; } = new List<MessageCounterDTO>();

        public List<UserHistoryTBL_VM> UserHistoryTBL_VM { get; set; } = new List<UserHistoryTBL_VM>();

    }
}
