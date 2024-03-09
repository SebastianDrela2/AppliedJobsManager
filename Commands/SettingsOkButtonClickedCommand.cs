using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Settings;
using AppliedJobsManager.ViewModels;
using AppliedJobsManager.Views;
using System.Windows.Input;

namespace AppliedJobsManager.Commands
{
    public class SettingsOkButtonClickedCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly AppliedJobsViewModel _appliedJobsViewModel;
        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly SettingsLoader _settingsLoader;

        public SettingsOkButtonClickedCommand
            (JsonSettingsManager jsonSettingsManager, SettingsLoader settingsLoader, AppliedJobsViewModel appliedJobsViewModel)
        {
            _jsonSettingsManager = jsonSettingsManager;
            _settingsLoader = settingsLoader;
            _appliedJobsViewModel = appliedJobsViewModel;
        }

        public bool CanExecute(object? parameter) => true;        
        public void Execute(object? parameter)
        {
            var view = (SettingsWindow) parameter!;

            var newSettings = new Settings.Settings
            {
                RemoveInvalidRows = (bool)view._invalidRowsCheckBox.IsChecked!,
                SaveColumnWidths = (bool)view._saveColumnsWidthsCheckBox.IsChecked!,
                JobsColumns = _appliedJobsViewModel.JobsColumns.Select(x => x.ActualWidth).ToList(),
                Font = view._fontsComboBox.SelectedItem.ToString()!,
                RowHightlightColor = view._rowHighlightColorTextbox.Background,
            };

            _jsonSettingsManager.SaveSettings(newSettings);
            _appliedJobsViewModel.RowHighlightColor = _settingsLoader.GetRowHightlightColor();

            view.Close();
        }
    }
}
