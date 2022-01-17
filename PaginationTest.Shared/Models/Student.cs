using Cosmonaut.Attributes;
using Newtonsoft.Json;

namespace PaginationTest.Shared.Models
{
    [CosmosCollection("students")]
    public class Student
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}