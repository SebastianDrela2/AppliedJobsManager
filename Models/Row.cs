using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace AppliedJobsManager.Models
{
    public class Row
    {      
        [NotNull]
        public required string Link { get; set; }
        public required string Job { get; set; }
        public required string Description { get; set; }
        public required string Pay { get; set; }
        public required string Date { get; set; }

        [JsonIgnore]
        public string? TypeOfWork { get; set; }
        public string? Experience { get; set;}
        public string? EmploymentType { get; set; }
        public string? OperatingMode { get; set; }
    }
}
