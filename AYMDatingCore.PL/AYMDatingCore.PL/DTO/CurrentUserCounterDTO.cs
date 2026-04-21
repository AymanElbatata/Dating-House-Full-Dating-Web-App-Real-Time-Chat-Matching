namespace AYMDatingCore.PL.DTO
{
    public class CurrentUserCounterDTO
    {
        public int CounterOfNewMessages { get; set; } = 0;
        public int CounterOfNewLikes { get; set; } = 0;
        public int CounterOfNewViews { get; set; } = 0;
        public int CounterOfNewFavorites { get; set; } = 0;
        public int CounterOfNewBlocks { get; set; } = 0;
    }
}
