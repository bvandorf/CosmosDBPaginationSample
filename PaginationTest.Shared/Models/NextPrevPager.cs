namespace PaginationTest.Shared.Models
{
    public class NextPrevPager
    {
        public string ContinuationToken { get; set; }

        public bool HasNextPage { get; set; }
    }
}