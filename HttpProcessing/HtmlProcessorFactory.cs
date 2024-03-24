namespace AppliedJobsManager.HttpProcessing
{
    public static class HtmlProcessorFactory
    {
        public static IHtmlProcessor CreateHtmlProcessor(string link)
        {
           return link switch
           {
              _ when link.StartsWith("https://justjoin.it/") => new JustJoinITHtmlProcessor(),
              _ when link.StartsWith("https://www.pracuj.pl/") => new PracujPlHtmlProcessor(),
               _ => new FakeHtmlProcessor()
           };
        }
    }
}
