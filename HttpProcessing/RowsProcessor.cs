using AppliedJobsManager.Models;

namespace AppliedJobsManager.HttpProcessing
{
    internal class RowsProcessor
    {
        private readonly IList<Row> _rows;
        public RowsProcessor(IList<Row> rows)
        {
            _rows = rows;
        }

        public void ProcessAdditionalInformation()
        {
            var tasks = new Task[_rows.Count];

            for (int i = 0; i < _rows.Count; i++)
            {
                var row = _rows[i];

                tasks[i] = Task.Run(() =>
                {
                    var htmlProcessor = new JustJoinITHtmlProcessor();
                    var httpRequest = htmlProcessor.GetRequest(row.Link);

                    htmlProcessor.SetAllDivs(httpRequest);

                    row.TypeOfWork = htmlProcessor.GetInnerDivHtml("Type of work");
                    row.Experience = htmlProcessor.GetInnerDivHtml("Experience");
                    row.EmploymentType = htmlProcessor.GetInnerDivHtml("Employment Type");
                    row.OperatingMode = htmlProcessor.GetInnerDivHtml("Operating mode");
                });
            }

            Task.WaitAll(tasks);
        }

        public async Task ProcessAdditionalInformationAsync()
        {
            var tasks = new List<Task>();

            for (var i = 0; i < _rows.Count; i++)
            {
                var row = _rows[i];

                tasks.Add(Task.Run(async () =>
                {
                    var htmlProcessor = new JustJoinITHtmlProcessor();
                    var httpRequest = await htmlProcessor.GetRequestAsync(row.Link);
                    htmlProcessor.SetAllDivs(httpRequest);

                    row.TypeOfWork = htmlProcessor.GetInnerDivHtml("Type of work");
                    row.Experience = htmlProcessor.GetInnerDivHtml("Experience");
                    row.EmploymentType = htmlProcessor.GetInnerDivHtml("Employment Type");
                    row.OperatingMode = htmlProcessor.GetInnerDivHtml("Operating mode");
                }));
            }

            await Task.WhenAll(tasks);
        }
    }
}
