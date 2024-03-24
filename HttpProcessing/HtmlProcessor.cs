using HtmlAgilityPack;
using System.Net.Http;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace AppliedJobsManager.HttpProcessing
{
    public abstract class HtmlProcessor
    {
        public HtmlNodeCollection GetAllNodes(string httpRequest, string node)
        {
            if (httpRequest is not null)
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(httpRequest);

                return htmlDocument.DocumentNode.SelectNodes($"//{node}");
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
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> GetRequestAsync(string requestUri)
        {
            using var httpClient = new HttpClient();

            try
            {
                return await httpClient.GetStringAsync(requestUri);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
