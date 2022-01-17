using PaginationTest.Shared.Models;
using System.Collections.Generic;

namespace PaginationTest.DemoDataSet
{
    public class SchoolRecords
    {
        public List<Student> Students { get; } = new List<Student>();

        public SchoolRecords()
        {
            for (var i = 0; i < 12; i++)
            {
                Students.Add(new Student() { Id = $"Id{i}", FirstName = $"FirstName {i}", LastName = $"LastName {i}" });
            }
        }
    }
}
