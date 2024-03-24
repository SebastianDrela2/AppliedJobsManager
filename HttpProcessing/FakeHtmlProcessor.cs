namespace AppliedJobsManager.HttpProcessing
{
    internal class FakeHtmlProcessor : HtmlProcessor, IHtmlProcessor
    {
        public string GetInnerHtml(string outerName)
        {
            return @"N\A";
        }
        
        public void SetRequiredInformation(string httpRequest)
        {
            // empty implementation.
        }
    }
}
