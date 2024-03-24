namespace AppliedJobsManager.HttpProcessing
{
    public static class HtmlProcessorFactory
    {
        public static IHtmlProcessor CreateHtmlProcessor(string link)
        {
            if (link.StartsWith("https://justjoin.it/"))
            {
                return new JustJoinITHtmlProcessor();
            }
            
            if (link.StartsWith("https://www.pracuj.pl"))
            {
                return new PracujPlHtmlProcessor();
            }

            return new FakeHtmlProcessor();
        }
    }
}
