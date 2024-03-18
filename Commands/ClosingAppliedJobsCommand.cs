using AppliedJobsManager.DataManagement;
using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Models;
using AppliedJobsManager.ViewModels;
using AppliedJobsManager.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppliedJobsManager.Commands
{
    public class ClosingAppliedJobsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly JsonJobsManager _jsonJobsManager;
        private readonly InvalidRowsRemover _invalidRowsRemover;
        private readonly InvalidRowsNotifier _invalidRowsNotifier;       
        private readonly AppliedJobsViewModel _appliedJobsViewModel;

        public ClosingAppliedJobsCommand
            (JsonSettingsManager jsonSettingsManager, JsonJobsManager jsonJobsManager,
            InvalidRowsRemover invalidRowsRemover, InvalidRowsNotifier invalidRowsNotifier, AppliedJobsViewModel appliedJobsViewModel)
        {
            _jsonSettingsManager = jsonSettingsManager;
            _invalidRowsRemover = invalidRowsRemover;
            _invalidRowsNotifier = invalidRowsNotifier;
            _jsonJobsManager = jsonJobsManager;           
            _appliedJobsViewModel = appliedJobsViewModel;
        }
        public bool CanExecute(object? parameter) => true;
        
        public void Execute(object? parameter)
        {
            var settings = _jsonSettingsManager.GetSettings();
            var view = (AppliedJobsView) parameter!;           

            if (settings.RemoveInvalidRows)
            {
                var previousItems = _appliedJobsViewModel.Rows.Cast<object>().ToList();
                var invalidRows = _invalidRowsRemover.ManageInvalidRows();

                _invalidRowsNotifier.Notify(invalidRows, previousItems);               
            }

            if (settings.SaveColumnWidths)
            {
                settings.JobsColumns = view._dataGrid.Columns.Select(x => x.ActualWidth).ToList();
            }

            settings.Window = new Rectangle((int)view.Left, (int)view.Top, (int)view.Width, (int)view.Height);

            _jsonSettingsManager.SaveSettings(settings);
            _jsonJobsManager.SaveJobs(_appliedJobsViewModel.Rows);
        }
    }
}
