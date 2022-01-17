using System.Collections.Generic;

namespace PaginationTest.Shared.Models
{
    public class NextPrevPagedData<T>
    {
        public IEnumerable<T> Items { get; set; }

        public NextPrevPager Pager { get; set; }
    }
}