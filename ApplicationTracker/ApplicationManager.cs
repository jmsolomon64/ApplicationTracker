using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApplicationTracker
{
    internal class AllApplications
    {
        [JsonProperty("applicaions")]
        public List<ApplicationItem>? Applications { get; set; }
    }

    internal class ApplicationItem
    {
        [JsonProperty("id")]
        public int? ID { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("languages")]
        public string Languages {get; set;}
        [JsonProperty("Location")]
        public string Location {get; set;}
        [JsonProperty("salary")]
        public decimal? Salary { get; set; }
        [JsonProperty("applied")]
        public DateTime Applied { get; set; }
        [JsonProperty("recruiter")]
        public string? Recruiter { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("open")]
        public bool Open { get; set; }
        [JsonProperty("followUps")]
        public List<FollowUp>? FollowUps { get; set; }

        public bool ConvertSalary(string input)
        {
            decimal output = 0m;
            bool success = decimal.TryParse(input, out output);
            Salary = output;
            return success;
        }
    }

    internal class FollowUp
    {
        public string? Description { get; set; }
        public DateTime Arrived { get; set; }
    }
    
}
