using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginationTest.Client
{
    public class AppSettings
    {
        public BackedApi BackedApi { get; set; }

        public AppSettings()
        {
            BackedApi = new BackedApi()
            {
                Hostname = "https://localhost:5001"
            };
        }
    }

    public class BackedApi
    {
        public string Hostname { get; set; }
    }
}
