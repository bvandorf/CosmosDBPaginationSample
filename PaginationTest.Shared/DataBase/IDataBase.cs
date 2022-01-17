using PaginationTest.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaginationTest.Shared.DataBase
{
    public interface IDataBase
    {
        IQueryable<Student> StudentsQuery(int page, int pageSize);
        int StudentsCount();
        int StudentsCount(Func<Student, bool> func);
        PagedResults<Student> StudentsToPagedList(string continuationToken, int pageSize);
    }
}
