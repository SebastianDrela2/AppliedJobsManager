using AppliedJobsManager.Models;
using System.Runtime.InteropServices;

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

                tasks[i] = Task.Run(() => PopulateRow(row));
            }

            Task.WaitAll(tasks);
        }

        public async Task ProcessAdditionalInformationAsync()
        {
            var tasks = new List<Task>();

            for (var i = 0; i < _rows.Count; i++)
            {
                var row = _rows[i];
                tasks.Add(Task.Run(async () => await PopulateRowAsync(row)));                                 
            }

            await Task.WhenAll(tasks);
        }

        private void PopulateRow(Row row)
        {
            var htmlProcessor = HtmlProcessorFactory.CreateHtmlProcessor(row.Link);
            var httpRequest = htmlProcessor.GetRequest(row.Link);

            htmlProcessor.SetRequiredInformation(httpRequest);

            row.TypeOfWork = htmlProcessor.GetInnerHtml("Type of work");
            row.Experience = htmlProcessor.GetInnerHtml("Experience");
            row.EmploymentType = htmlProcessor.GetInnerHtml("Employment Type");
            row.OperatingMode = htmlProcessor.GetInnerHtml("Operating mode");
        }

        private async Task PopulateRowAsync(Row row)
        {
            var htmlProcessor = HtmlProcessorFactory.CreateHtmlProcessor(row.Link);
            var httpRequest = await htmlProcessor.GetRequestAsync(row.Link);

            htmlProcessor.SetRequiredInformation(httpRequest);

            row.TypeOfWork = htmlProcessor.GetInnerHtml("Type of work");
            row.Experience = htmlProcessor.GetInnerHtml("Experience");
            row.EmploymentType = htmlProcessor.GetInnerHtml("Employment Type");
            row.OperatingMode = htmlProcessor.GetInnerHtml("Operating mode");
        }
    }
}
