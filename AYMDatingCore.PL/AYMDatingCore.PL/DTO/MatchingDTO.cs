namespace AYMDatingCore.PL.DTO
{
    public class MatchingDTO
    {
        public string? AppUserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Country { get; set; }
        public string? Purpose { get; set; }
        public int? Age { get; set; }
        public string? ProfileHeading { get; set; } = string.Empty;
        public string? MainImageUrl { get; set; } = string.Empty;
        public DateTime DateOfJoin { get; set; } = DateTime.Now;

    }
}
