using AppliedJobsManager.JsonProcessing;
using System.Windows;

namespace AppliedJobsManager.UI
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly Settings.Settings _settings;
        public SettingsWindow(Settings.Settings settings, JsonSettingsManager jsonSettingsManager)
        {
            InitializeComponent();

            _settings = settings;
            _jsonSettingsManager = jsonSettingsManager;

            _invalidRowsCheckBox.IsChecked = _settings.RemoveInvalidRows;
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            var settingsToSave = new Settings.Settings { RemoveInvalidRows = (bool)_invalidRowsCheckBox.IsChecked};
            _jsonSettingsManager.SaveSettings(settingsToSave);

            Close();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
