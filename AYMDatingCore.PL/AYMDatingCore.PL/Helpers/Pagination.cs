namespace AYMDatingCore.PL.Helpers
{
    public class Pagination<T>
    {
        public List<T> MyList { get; set; } = new List<T>();

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int Skip => (CurrentPage - 1) * PageSize;


        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;


        public int TotalCount { get; set; }
        public string? ViewName { get; set; }

    }

}
