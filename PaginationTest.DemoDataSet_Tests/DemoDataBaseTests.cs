using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaginationTest.DemoDataSet;
using PaginationTest.Shared.DataBase;
using PaginationTest.Shared.Models;
using System.Linq;

namespace PaginationTest.DemoDataSet_Tests
{
    [TestClass]
    public class DemoDataBaseTests
    {
        [TestMethod]
        public void StudentsQuery_AllStudents()
        {
            var schoolRecords = new SchoolRecords();
            var demoDB = new DataBase();


            var studentRecords = schoolRecords.Students;
            var demoStudentRecords = demoDB.StudentsQuery(1, schoolRecords.Students.Count);


            AssertJson(studentRecords, demoStudentRecords);
        }

        [TestMethod]
        public void StudentsQuery_Page2()
        {
            var schoolRecords = new SchoolRecords();
            var demoDB = new DataBase();


            var studentRecords = schoolRecords.Students.Skip(2).Take(2);
            var demoStudentRecords = demoDB.StudentsQuery(2, 2);


            AssertJson(studentRecords, demoStudentRecords);
        }

        [TestMethod]
        public void StudentsCount_All()
        {
            var schoolRecords = new SchoolRecords();
            var demoDB = new DataBase();


            var studentRecordsCount = schoolRecords.Students.Count;
            var demoStudentRecordsCount = demoDB.StudentsCount();


            Assert.AreEqual(studentRecordsCount, demoStudentRecordsCount);
        }

        [TestMethod]
        public void StudentsCount_Id1ThruId3()
        {
            var schoolRecords = new SchoolRecords();
            var demoDB = new DataBase();


            var validIds = new string[] { "Id1", "Id2", "Id3" };
            var studentRecordsCount = schoolRecords.Students.Count(x => validIds.Contains(x.Id));
            var demoStudentRecordsCount = demoDB.StudentsCount(x => validIds.Contains(x.Id));


            Assert.AreEqual(studentRecordsCount, demoStudentRecordsCount);
        }

        [TestMethod]
        public void StudentsToPagedList_All()
        {
            var schoolRecords = new SchoolRecords();
            var demoDB = new DataBase();


            var pagedResultsList = new PagedResults<Student>()
            {
                Results = schoolRecords.Students.AsQueryable(),
                NextPageToken = "",
                HasNextPage = false
            };

            var demoStudentRecords = demoDB.StudentsToPagedList("", schoolRecords.Students.Count);


            AssertJson(pagedResultsList, demoStudentRecords);
        }

        [TestMethod]
        public void StudentsToPagedList_EmptyContinuationTokenTake3()
        {
            var schoolRecords = new SchoolRecords();
            var demoDB = new DataBase();

            var students = schoolRecords.Students.AsQueryable().Take(3);
            var pagedResultsList = new PagedResults<Student>()
            {
                Results = students,
                NextPageToken = "2",
                HasNextPage = true
            };

            var demoStudentRecords = demoDB.StudentsToPagedList("", 3);


            AssertJson(pagedResultsList, demoStudentRecords);
        }

        [TestMethod]
        public void StudentsToPagedList_Skip0Take3()
        {
            var schoolRecords = new SchoolRecords();
            var demoDB = new DataBase();

            var students = schoolRecords.Students.AsQueryable().Take(3);
            var pagedResultsList = new PagedResults<Student>()
            {
                Results = students,
                NextPageToken = "2",
                HasNextPage = true
            };

            var demoStudentRecords = demoDB.StudentsToPagedList("1", 3);


            AssertJson(pagedResultsList, demoStudentRecords);
        }

        [TestMethod]
        public void StudentsToPagedList_Skip3Take3()
        {
            var schoolRecords = new SchoolRecords();
            var demoDB = new DataBase();

            var students = schoolRecords.Students.AsQueryable().Skip(3).Take(3);
            var pagedResultsList = new PagedResults<Student>()
            {
                Results = students,
                NextPageToken = "3",
                HasNextPage = true
            };

            var demoStudentRecords = demoDB.StudentsToPagedList("2", 3);


            AssertJson(pagedResultsList, demoStudentRecords);
        }

        [TestMethod]
        public void StudentsToPagedList_Skip6Take3()
        {
            var schoolRecords = new SchoolRecords();
            var demoDB = new DataBase();

            var students = schoolRecords.Students.AsQueryable().Skip(6).Take(3);
            var pagedResultsList = new PagedResults<Student>()
            {
                Results = students,
                NextPageToken = "4",
                HasNextPage = true
            };

            var demoStudentRecords = demoDB.StudentsToPagedList("3", 3);


            AssertJson(pagedResultsList, demoStudentRecords);
        }

        [TestMethod]
        public void StudentsToPagedList_Skip9Take3()
        {
            var schoolRecords = new SchoolRecords();
            var demoDB = new DataBase();

            var students = schoolRecords.Students.AsQueryable().Skip(9).Take(3);
            var pagedResultsList = new PagedResults<Student>()
            {
                Results = students,
                NextPageToken = "",
                HasNextPage = false
            };

            var demoStudentRecords = demoDB.StudentsToPagedList("4", 3);


            AssertJson(pagedResultsList, demoStudentRecords);
        }

        private void AssertJson(object objExpected, object objActual)
        {
            var jsonExpected = Newtonsoft.Json.JsonConvert.SerializeObject(objExpected);
            var jsonActual = Newtonsoft.Json.JsonConvert.SerializeObject(objActual);

            Assert.AreEqual(jsonExpected, jsonActual);
        }

    }
}
