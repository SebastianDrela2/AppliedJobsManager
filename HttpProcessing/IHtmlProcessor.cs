namespace AppliedJobsManager.HttpProcessing
{
    public interface IHtmlProcessor
    {
        public string GetInnerHtml(string outerName);
        public void SetRequiredInformation(string httpRequest);
        public string GetRequest(string requestUri);
        public Task<string> GetRequestAsync(string requestUri);
    }
}
