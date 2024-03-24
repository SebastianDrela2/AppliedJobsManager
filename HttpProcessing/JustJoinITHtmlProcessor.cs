using HtmlAgilityPack;

namespace AppliedJobsManager.HttpProcessing
{
    public class JustJoinITHtmlProcessor : HtmlProcessor, IHtmlProcessor
    {
        private HtmlNodeCollection? _divs;

        public string GetInnerHtml(string outerName)
        {
            var typeDiv = _divs!.First(x => x.InnerHtml == outerName);
            var valueTypeIndex = _divs!.IndexOf(typeDiv) + 1;
            var desiredDiv = _divs[valueTypeIndex];

            return desiredDiv.InnerHtml;
        }

        public void SetRequiredInformation(string httpRequest) => _divs = GetAllNodes(httpRequest, "div");      
    }
}
