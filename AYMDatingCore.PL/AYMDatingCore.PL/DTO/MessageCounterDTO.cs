namespace AYMDatingCore.PL.DTO
{
    public class MessageCounterDTO
    {
        public string? AppUserId { get; set; }
        public int? Counter { get; set; } = 0;
        public DateTime? LatestMessageDate { get; set; }

    }
}
