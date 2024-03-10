using AppliedJobsManager.HttpProcessing;
using Newtonsoft.Json;

namespace AppliedJobsManager.Models
{
    public class Row
    {
        private JustJoinITHtmlProcessor _htmlProcessor;
        
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

        public string Job { get; set; }
        public string Description { get; set; }
        public string Pay { get; set; }
        public string Date { get; set; }

        [JsonIgnore]
        public string TypeOfWork => _htmlProcessor.GetInnerDivHtml("Type of work");
        public string Experience => _htmlProcessor.GetInnerDivHtml("Experience");
        public string EmploymentType => _htmlProcessor.GetInnerDivHtml("Employment Type");
        public string OperatingMode => _htmlProcessor.GetInnerDivHtml("Operating mode");
    }
}
