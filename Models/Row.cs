using AppliedJobsManager.HttpProcessing;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace AppliedJobsManager.Models
{
    public class Row
    {
        private JustJoinITHtmlProcessor _htmlProcessor;

        [NotNull]
        private string _link;
        public string Link
        {
            get
            {
                if (_htmlProcessor is null)
                {
                    _htmlProcessor = new JustJoinITHtmlProcessor(_link);
                }

                return _link;
            }
            set => _link = value;
        }

        public required string Job { get; set; }
        public required string Description { get; set; }
        public required string Pay { get; set; }
        public required string Date { get; set; }

        [JsonIgnore]
        public string TypeOfWork => _htmlProcessor.GetInnerDivHtml("Type of work");
        public string Experience => _htmlProcessor.GetInnerDivHtml("Experience");
        public string EmploymentType => _htmlProcessor.GetInnerDivHtml("Employment Type");
        public string OperatingMode => _htmlProcessor.GetInnerDivHtml("Operating mode");
    }
}
