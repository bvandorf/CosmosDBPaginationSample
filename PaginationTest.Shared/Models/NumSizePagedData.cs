using System.Collections.Generic;

namespace PaginationTest.Shared.Models
{
    public class NumSizePagedData<T>
    {
        public IEnumerable<T> Items { get; set; }

        public NumSizePager NumSizePager { get; set; }
    }
}