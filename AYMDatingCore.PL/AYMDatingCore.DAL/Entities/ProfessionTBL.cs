using AYMDatingCore.DAL.BaseEntity;

namespace AYMDatingCore.DAL.Entities
{
    public class ProfessionTBL : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        //public virtual ICollection<UserHistory> UserHistories { get; set; }
    }
}