using AYMDatingCore.DAL.BaseEntity;

namespace AYMDatingCore.DAL.Entities
{
    public class ContactUsTBL : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
       
    }
}