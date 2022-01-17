using System.Linq;

namespace PaginationTest.Shared.DataBase
{
    public class PagedResults<T>
    {
        public IQueryable<T> Results { get; set; }
        public string NextPageToken { get; set; }
        public bool HasNextPage { get; set; }
    }
}
