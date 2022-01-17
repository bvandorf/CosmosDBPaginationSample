using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaginationTest.Shared;
using System;

namespace PaginationTest.Shared_Tests
{
    [TestClass]
    public class NumSizePagerTests
    {
        [TestMethod]
        public void PagerSize1()
        {
            var totalItems = 12;
            var page = 1;
            var pageSize = 3;

            var assertTotalPages = 4;
            var assertStartPage = 1;
            var assertEndPage = 4;
            var assertSuccess = true;

            var pager = new NumSizePager(totalItems, page, pageSize);

            AssertNumSizePager(pager, totalItems, page, pageSize, assertTotalPages, assertStartPage, assertEndPage, assertSuccess);
        }

        [TestMethod]
        public void PagerSize2()
        {
            var totalItems = 13;
            var page = 2;
            var pageSize = 4;

            var assertTotalPages = 4;
            var assertStartPage = 1;
            var assertEndPage = 4;
            var assertSuccess = true;

            var pager = new NumSizePager(totalItems, page, pageSize);

            AssertNumSizePager(pager, totalItems, page, pageSize, assertTotalPages, assertStartPage, assertEndPage, assertSuccess);
        }

        [TestMethod]
        public void PagerSize_NullPageNumber()
        {
            var totalItems = 13;
            int? page = null;
            var pageSize = 4;

            var assertSuccess = true;

            ActAndAssertNumSizePager(totalItems, page, pageSize, assertSuccess);
        }

        [TestMethod]
        public void PagerSize_ZeroPageNumber()
        {
            var totalItems = 10;
            var page = 0;
            var pageSize = 4;

            var assertSuccess = true;

            ActAndAssertNumSizePager(totalItems, page, pageSize, assertSuccess);
        }

        [TestMethod]
        public void PagerSize_NegativePageNumber()
        {
            var totalItems = 10;
            var page = -5;
            var pageSize = 4;

            var assertSuccess = true;

            ActAndAssertNumSizePager(totalItems, page, pageSize, assertSuccess);
        }

        [TestMethod]
        public void PagerSize_ZeroPageSizeNumber()
        {
            var totalItems = 10;
            var page = 1;
            var pageSize = 0;

            var assertSuccess = false;

            ActAndAssertNumSizePager(totalItems, page, pageSize, assertSuccess);
        }

        [TestMethod]
        public void PagerSize_NegativePageSizeNumber()
        {
            var totalItems = 10;
            var page = 1;
            var pageSize = -2;

            var assertSuccess = false;

            ActAndAssertNumSizePager(totalItems, page, pageSize, assertSuccess);
        }

        [TestMethod]
        public void PagerSize_OneItemsNumber()
        {
            var totalItems = 1;
            var page = 1;
            var pageSize = 3;

            var assertSuccess = true;

            ActAndAssertNumSizePager(totalItems, page, pageSize, assertSuccess);
        }

        [TestMethod]
        public void PagerSize_ZeroItemsNumber()
        {
            var totalItems = 0;
            var page = 1;
            var pageSize = 3;

            var assertSuccess = true;

            ActAndAssertNumSizePager(totalItems, page, pageSize, assertSuccess);
        }

        [TestMethod]
        public void PagerSize_NegativeItemsNumber()
        {
            var totalItems = -9;
            var page = 1;
            var pageSize = 3;

            var assertSuccess = false;

            ActAndAssertNumSizePager(totalItems, page, pageSize, assertSuccess);
        }

        private void ActAndAssertNumSizePager(int totalItems, int? page, int pageSize, bool assertSuccess)
        {
            var assertVars = LocalPageCalc(assertSuccess, totalItems, page, pageSize);
            var assertTotalPages = assertVars.TotalPages;
            var assertPage = assertVars.Page;
            var assertStartPage = assertVars.StartPage;
            var assertEndPage = assertVars.EndPage;
            var assertTotalItems = assertVars.TotalItems;
            var assertPageSize = assertVars.PageSize;

            var pager = new NumSizePager(totalItems, page, pageSize);

            AssertNumSizePager(pager, assertTotalItems, assertPage, assertPageSize, assertTotalPages, assertStartPage, assertEndPage, assertSuccess);
        }

        private void AssertNumSizePager(NumSizePager pager, int totalItems, int? page, int pageSize, int totalPages, int startPage, int endPage, bool success)
        {
            Assert.AreEqual(totalItems, pager.TotalItems);
            Assert.AreEqual(page, pager.CurrentPage);
            Assert.AreEqual(pageSize, pager.PageSize);
            Assert.AreEqual(totalPages, pager.TotalPages);
            Assert.AreEqual(startPage, pager.StartPage);
            Assert.AreEqual(endPage, pager.EndPage);
            Assert.AreEqual(success, pager.Success);
        }


        private class LocalPageCalcResult
        {
            public bool Success { get; set; }
            public int TotalItems { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
            public int TotalPages { get; set; }
            public int StartPage { get; set; }
            public int EndPage { get; set; }
        }

        private LocalPageCalcResult LocalPageCalc(bool assertSuccess, int totalItems, int? page, int pageSize)
        {
            if (!page.HasValue || !assertSuccess)
                page = assertSuccess ? 1 : 0;
            else if (page < 0)
                page = 1;

            var assertTotalItems = assertSuccess ? totalItems : 0;
            var assertPage = assertSuccess ? page.Value : 0;
            var assertPageSize = assertSuccess ? pageSize : 0;

            var dec = ((double)totalItems / (double)pageSize) + 0.99;
            var assertTotalPages = assertSuccess ? (int)dec : 0;
            var assertStartPage = assertSuccess ? 1 : 0;
            var assertEndPage = assertTotalPages;

            return new LocalPageCalcResult()
            {
                Success = assertSuccess,
                TotalItems = assertTotalItems,
                Page = assertPage,
                PageSize = assertPageSize,
                TotalPages = assertTotalPages,
                StartPage = assertStartPage,
                EndPage = assertEndPage
            };
        }
    }
}
