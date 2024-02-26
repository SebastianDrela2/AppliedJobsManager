using AppliedJobsManager.JsonProcessing;
using System.Windows;
using System.Windows.Controls;

namespace AppliedJobsManager.UI
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly Settings.Settings _settings;
        private readonly DataGrid _dataGrid;
        public SettingsWindow(Settings.Settings settings, JsonSettingsManager jsonSettingsManager, DataGrid dataGrid)
        {
            InitializeComponent();

            _settings = settings;
            _jsonSettingsManager = jsonSettingsManager;
            _dataGrid = dataGrid;

            SetUI();
        }

        private void SetUI()
        {
            _invalidRowsCheckBox.IsChecked = _settings.RemoveInvalidRows;
            _saveColumnsWidths.IsChecked = _settings.SaveColumnWidths;
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            var settingsToSave = new Settings.Settings
            {
                RemoveInvalidRows = (bool)_invalidRowsCheckBox.IsChecked,
                SaveColumnWidths = (bool)_saveColumnsWidths.IsChecked,
                ColumnsWidths = _dataGrid.Columns.Select(x => x.ActualWidth).ToList()
                };

            _jsonSettingsManager.SaveSettings(settingsToSave);

            Close();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
