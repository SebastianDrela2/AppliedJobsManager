using HtmlAgilityPack;

namespace AppliedJobsManager.HttpProcessing
{
    public class PracujPlHtmlProcessor : HtmlProcessor, IHtmlProcessor
    {
        private IEnumerable<HtmlNode> _lis;

        private readonly Dictionary<string, string> _typeNameDict = new Dictionary<string, string>()
        {
            { "Employment Type", "contract-types" },
            { "Type of work", "work-schedules" },
            { "Experience", "position-levels" },
            { "Operating mode", "work-modes" }
        };
        
        public string GetInnerHtml(string outerName)
        {
            _typeNameDict.TryGetValue(outerName, out var htmlRelatedName);

            var relatedLi = _lis.First(x => x.InnerHtml.Contains(htmlRelatedName!));
            var desiredDiv = relatedLi.ChildNodes.First().ChildNodes.First(x => x.Name.Contains("div"));

            return desiredDiv.InnerText;

        }

        public void SetRequiredInformation(string httpRequest)
        {
            _lis = GetAllNodes(httpRequest, "li").Where(x => x.OuterHtml.Contains("sections-benefit-list-item"));           
        }
    }
}
