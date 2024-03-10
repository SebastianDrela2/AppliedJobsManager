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
                Font = view._fontsComboBox.SelectedItem.ToString()!,
                RowHightlightColor = view._rowHighlightColorTextbox.Background,
                RowFontColor = view._rowFontColorTextbox.Background
            };

            if (newSettings.SaveColumnWidths)
            {
                newSettings.JobsColumns = _jsonSettingsManager.GetSettings().JobsColumns;
            }

            _jsonSettingsManager.SaveSettings(newSettings);
            _settingsLoader.UpdateSettings();

            _appliedJobsViewModel.RowHighlightColor = _settingsLoader.GetRowHightlightColor();
            _appliedJobsViewModel.RowFontColor = _settingsLoader.GetRowFontColor();
            _appliedJobsViewModel.Font = _settingsLoader.GetFontFamily();

            view.Close();
        }
    }
}
