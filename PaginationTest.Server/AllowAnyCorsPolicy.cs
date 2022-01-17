using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginationTest.Server
{
    public class AllowAnyCorsPolicy : CorsPolicy
    {
        public AllowAnyCorsPolicy()
        {
            Origins.Clear();
            IsOriginAllowed = origin => true;
            Headers.Clear();
            Headers.Add("*");

            Methods.Clear();
            Methods.Add("*");

            SupportsCredentials = true;
        }
    }
}
