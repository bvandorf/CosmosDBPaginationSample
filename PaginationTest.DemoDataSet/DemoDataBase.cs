using PaginationTest.Shared.DataBase;
using PaginationTest.Shared.Models;
using System;
using System.Linq;

namespace PaginationTest.DemoDataSet.DataBase
{
    public class DataBase : IDataBase
    {
        readonly SchoolRecords schoolDB = new SchoolRecords();

        public IQueryable<Student> StudentsQuery(int page, int pageSize)
        {
            return schoolDB.Students.Skip((page -1) * pageSize).Take(pageSize).AsQueryable();
        }

        public int StudentsCount()
        {
            return schoolDB.Students.Count;
        }

        public int StudentsCount(Func<Student, bool> func)
        {
            return schoolDB.Students.Count(func);
        }

        public PagedResults<Student> StudentsToPagedList(string continuationToken, int pageSize)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(continuationToken))
                    continuationToken = "1";

                var page = int.Parse(continuationToken);
                var students = StudentsQuery(page, pageSize);
                var hasNextPage = StudentsQuery(page + 1, pageSize).Any();
                var nextPageToken = hasNextPage ? (page + 1).ToString() : "";

                return new PagedResults<Student>()
                {
                    Results = students.AsQueryable(),
                    HasNextPage = hasNextPage,
                    NextPageToken = nextPageToken
                };
            }
            catch
            {
                return new PagedResults<Student>()
                {
                    Results = null,
                    HasNextPage = false,
                    NextPageToken = ""
                };
            }
        }
    }
}
