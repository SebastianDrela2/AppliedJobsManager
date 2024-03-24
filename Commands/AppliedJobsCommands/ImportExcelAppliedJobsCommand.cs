using AppliedJobsManager.Excel;
using AppliedJobsManager.HttpProcessing;
using AppliedJobsManager.ViewModels;

namespace AppliedJobsManager.Commands.AppliedJobsCommands
{
    internal class ImportExcelAppliedJobsCommand
    {
        private readonly AppliedJobsViewModel _appliedJobsViewModel;
        
        public ImportExcelAppliedJobsCommand(AppliedJobsViewModel appliedJobsViewModel)
        {
            _appliedJobsViewModel = appliedJobsViewModel;
        }
        
        public async Task ExecuteAsync()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|All Files (*.*)|*.*"
            };

            string fileName = string.Empty;

            if (dialog.ShowDialog() is DialogResult.OK)
            {
                fileName = dialog.FileName;

                var excelProcessor = new ExcelProcessor();
                var rows = excelProcessor.Import(fileName);

                var rowsProcessor = new RowsProcessor(rows);
                await rowsProcessor.ProcessAdditionalInformationAsync();

                _appliedJobsViewModel.Rows = rows;
            }
        }
    }
}
