using HtmlAgilityPack;
using System.Net.Http;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace AppliedJobsManager.HttpProcessing
{
    public class JustJoinITHtmlProcessor
    {
        private HtmlNodeCollection? _divs;
        public string GetInnerDivHtml(string outerDivName)
        {
            if (_divs != null)
            {
                var typeDiv = _divs!.First(x => x.InnerHtml == outerDivName);
                var valueTypeIndex = _divs!.IndexOf(typeDiv) + 1;
                var desiredDiv = _divs[valueTypeIndex];

                return desiredDiv.InnerHtml;
            }

            return @"N\A";
        }

        public void SetAllDivs(string httpRequest)
        {
            _divs = GetAllDivs(httpRequest);
        }

        public HtmlNodeCollection GetAllDivs(string httpRequest)
        {
            if (httpRequest is not null)
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(httpRequest);

                return htmlDocument.DocumentNode.SelectNodes("//div");
            }

            return null;
        }
            
        public string GetRequest(string requestUri)
        {
            using var httpClient = new HttpClient();

            try
            {
                return httpClient.GetStringAsync(requestUri).Result;
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }
    }
}
