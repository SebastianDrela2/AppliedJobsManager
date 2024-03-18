using AppliedJobsManager.Excel;
using AppliedJobsManager.ViewModels;
using System.Windows.Input;

namespace AppliedJobsManager.Commands
{
    internal class ImportExcelAppliedJobsCommand : ICommand
    {
        private readonly AppliedJobsViewModel _appliedJobsViewModel;

        public event EventHandler? CanExecuteChanged;

        public ImportExcelAppliedJobsCommand(AppliedJobsViewModel appliedJobsViewModel)
        {
            _appliedJobsViewModel = appliedJobsViewModel;
        }
     
        public bool CanExecute(object? parameter) => true;      
        public void Execute(object? parameter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|All Files (*.*)|*.*"
            };

            string fileName = string.Empty;

            if (dialog.ShowDialog() is DialogResult.OK)
            {
                fileName = dialog.FileName;
            }

            var excelProcessor = new ExcelProcessor(fileName);
            var rows = excelProcessor.Process();

            _appliedJobsViewModel.Rows = rows;       
        }
    }
}
