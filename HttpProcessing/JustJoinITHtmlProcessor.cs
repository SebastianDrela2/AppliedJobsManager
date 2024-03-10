using HtmlAgilityPack;
using System.Net.Http;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace AppliedJobsManager.HttpProcessing
{
    public class JustJoinITHtmlProcessor
    {        
        private readonly string? _httpRequest;
        private readonly HtmlNodeCollection _divs;
      
        public JustJoinITHtmlProcessor(string httpLink)
        {           
            _httpRequest = GetRequest(httpLink);

            if (_httpRequest is not null)
            {
                _divs = GetAllDivs();
            }
        }

        public string GetInnerDivHtml(string outerDivName)
        {
            if (_httpRequest is null)
            {
                return "N/A";
            }
            
            var typeDiv = _divs.FirstOrDefault(x => x.InnerHtml == outerDivName);
            var valueTypeIndex = _divs.IndexOf(typeDiv) + 1;
            var desiredDiv = _divs[valueTypeIndex];
           
            return desiredDiv.InnerHtml;
        }

        private HtmlNodeCollection GetAllDivs()
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(_httpRequest);

            return htmlDocument.DocumentNode.SelectNodes("//div");
        }
            
        private string? GetRequest(string requestUri)
        {
            var httpClient = new HttpClient();

            try
            {
                return httpClient.GetStringAsync(requestUri).Result;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
