using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Settings;
using AppliedJobsManager.ViewModels;
using AppliedJobsManager.Views;
using System.Windows.Input;

namespace AppliedJobsManager.Commands.AppliedJobsCommands
{
    public class SettingsClickedAppliedJobsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly AppliedJobsViewModel _appliedJobsViewModel;
        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly SettingsLoader _settingsLoader;
        private Settings.Settings _settings;

        public SettingsClickedAppliedJobsCommand
            (JsonSettingsManager jsonSettingsManager, SettingsLoader settingsLoader, Settings.Settings settings, AppliedJobsViewModel appliedJobsViewModel)
        {
            _jsonSettingsManager = jsonSettingsManager;
            _settingsLoader = settingsLoader;
            _settings = settings;
            _appliedJobsViewModel = appliedJobsViewModel;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _settings = _jsonSettingsManager.GetSettings();

            var settingsWindow = new SettingsWindow(_jsonSettingsManager, _settingsLoader, _settings, _appliedJobsViewModel);
            settingsWindow.Show();
        }
    }
}
