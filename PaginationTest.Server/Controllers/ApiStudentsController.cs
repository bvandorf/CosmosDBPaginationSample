using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaginationTest.Shared;
using PaginationTest.Shared.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PaginationTest.Server.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class ApiStudentsController : ControllerBase
    {
        private readonly ILogger<ApiStudentsController> _logger;
        readonly DemoDataSet.DataBase.DataBase dataBase;

        public ApiStudentsController(ILogger<ApiStudentsController> logger)
        {
            logger.LogInformation("ApiStudentsController starting");

            _logger = logger;

            dataBase = new DemoDataSet.DataBase.DataBase();

            _logger.LogInformation("ApiStudentsController started");
        }

        [EnableCors(nameof(AllowAnyCorsPolicy))]
        [HttpGet("skiptake")]
        public Task<NumSizePagedData<Student>> GetStudentsNumSize([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation($"GetStudentsNumSize page {page} pageSize {pageSize}");


            var students = dataBase.StudentsQuery(page, pageSize).ToList(); 
            var totalCount = dataBase.StudentsCount(); 
            var pager = new NumSizePager(totalCount, page, pageSize);

            var response = new NumSizePagedData<Student>
            {
                Items = students,
                NumSizePager = pager
            };

            _logger.LogInformation($"GetStudentsNumSize response: Items.Count() {response.Items.Count()} NumSizePage.CurrentPage {response.NumSizePager.CurrentPage}");
            return Task.FromResult(response);
        }

        [EnableCors(nameof(AllowAnyCorsPolicy))]
        [HttpGet("nextprev")]
        public Task<NextPrevPagedData<Student>> GetStudentsNextPrev([FromQuery] string continuationToken = "", [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation($"GetStudentsNextPrev continuationToken {continuationToken} pageSize {pageSize}");

            var students = dataBase.StudentsToPagedList(continuationToken, pageSize);

            var response = new NextPrevPagedData<Student>
            {
                Items = students.Results,
                Pager = new NextPrevPager { ContinuationToken = students.NextPageToken, HasNextPage = students.HasNextPage }
            };

            _logger.LogInformation($"GetStudentsNextPrev response: Items.Count() {response.Items.Count()} Pager.ContinuationToken {response.Pager.ContinuationToken} Pager.HasNextPage {response.Pager.HasNextPage}");
            return Task.FromResult(response);
        }
    }
}
