using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTracker
{
    internal class AllApplications
    {
        public List<ApplicationItem> Applications { get; set; }
    }

    internal class ApplicationItem
    {
        public int? ID { get; set; }
        public string Title { get; set; }
        public decimal? Salary { get; set; }
        public DateTime Applied { get; set; }
        public string? Recruiter { get; set; }
        public string? Email { get; set; }
        public bool Open { get; set; }
        public List<FollowUp>? FollowUps { get; set; }
    }

    internal class FollowUp
    {
        public string Description { get; set; }
        public DateTime Arrived { get; set; }
    }
    
}
