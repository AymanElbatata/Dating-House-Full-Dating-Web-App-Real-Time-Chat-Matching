using AYMDatingCore.DAL.BaseEntity;

namespace AYMDatingCore.DAL.Entities
{
    public class PurposeTBL : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        //public virtual ICollection<UserHistory> UserHistories { get; set; }
    }
}