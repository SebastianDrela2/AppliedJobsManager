using AppliedJobsManager.DataManagement;
using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Utils;
using AppliedJobsManager.ViewModels;
using AppliedJobsManager.Views;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace AppliedJobsManager.Commands.AppliedJobsCommands
{
    public class ClosingAppliedJobsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private const string MessageBoxMessage = "Detected unsaved rows, Save modified rows?";

        private readonly AppliedJobsViewModel _appliedJobsViewModel;
        private readonly JsonSettingsManager _jsonSettingsManager;               
        private readonly JobsUpdater _jobsUpdater;

        public ClosingAppliedJobsCommand
            (JsonSettingsManager jsonSettingsManager, 
            JobsUpdater jobsUpdater, AppliedJobsViewModel appliedJobsViewModel)
        {          
            _jsonSettingsManager = jsonSettingsManager;
            _jobsUpdater = jobsUpdater;
            _appliedJobsViewModel = appliedJobsViewModel;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) => SaveData(parameter);
        
        private void SaveData(object? parameter)
        {         
            var settings = _jsonSettingsManager.GetSettings();
            var view = (AppliedJobsView)parameter!;

            if (_appliedJobsViewModel.RowsAreOutdated)
            {
                var messageBoxResult = MessageBox.Show(MessageBoxMessage, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (messageBoxResult is MessageBoxResult.Yes)
                {
                    _jobsUpdater.UpdateJobs();
                }
            }

            if (settings.SaveColumnWidths)
            {
                settings.JobsColumns = view._dataGrid.Columns.Select(x => x.ActualWidth).ToList();
            }

            settings.Window = RectangleUtils.GetRectangleFromWindow(view);

            _jsonSettingsManager.SaveSettings(settings);
        }        
    }
}
