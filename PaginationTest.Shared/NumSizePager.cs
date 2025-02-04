﻿using System;

namespace PaginationTest.Shared
{
    public class NumSizePager
    {
        public NumSizePager(){}

        public NumSizePager(int totalItems, int? page, int pageSize = 10)
        {
            if (page < 0)
                page = null;

            if (pageSize < 1)
            {
                Success = false;
                return;
            }

            if (totalItems < 0)
            {
                Success = false;
                return;
            }

            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page ?? 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public bool Success { get; set; } = true;
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}