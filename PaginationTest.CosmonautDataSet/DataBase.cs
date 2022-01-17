﻿using Cosmonaut.Extensions;
using Microsoft.Azure.Cosmos;
using PaginationTest.Shared.DataBase;
using PaginationTest.Shared.Models;
using System;
using System.Linq;

namespace PaginationTest.CosmonautDataSet
{
    public class DataBase : IDataBase
    {
        readonly string _cosmosConnectionString = "AccountEndpoint=https://xxx.documents.azure.com:443/;AccountKey=xxx;";
        readonly string _cosmosDatabaseName = "SchoolRecords";
        readonly string _cosmosStudentsContainerName = "Students";

        readonly ContainerResponse students;

        public DataBase()
        {
            var cosmonautClient = new CosmosClient(_cosmosConnectionString);
            var db = cosmonautClient.CreateDatabaseIfNotExistsAsync(_cosmosDatabaseName).Result;
            students = db.Database.CreateContainerIfNotExistsAsync(_cosmosStudentsContainerName, "id").Result;
        }

        public IQueryable<Student> StudentsQuery(int page, int pageSize)
        {
            return students.Container.GetItemLinqQueryable<Student>().WithPagination(page, pageSize);
        }

        public int StudentsCount()
        {
            return students.Container.GetItemLinqQueryable<Student>().Count();
        }
        public int StudentsCount(Func<Student, bool> func)
        {
            return students.Container.GetItemLinqQueryable<Student>().Count(func);
        }

        public PagedResults<Student> StudentsToPagedList(string continuationToken, int pageSize)
        {
            try
            {
                var studentsQuery = students.Container.GetItemLinqQueryable<Student>().WithPagination(continuationToken, pageSize).ToPagedListAsync().Result;

                var result = new PagedResults<Student>();
                result.Results = studentsQuery.Results.AsQueryable();
                result.HasNextPage = studentsQuery.HasNextPage;
                result.NextPageToken = studentsQuery.NextPageToken;

                return result;
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
