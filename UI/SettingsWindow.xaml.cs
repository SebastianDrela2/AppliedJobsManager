using AppliedJobsManager.JsonProcessing;
using System.Windows;
using System.Windows.Controls;
using System.Drawing.Text;
using System.Windows.Media;
using AppliedJobsManager.Settings;

namespace AppliedJobsManager.UI
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly JsonSettingsManager _jsonSettingsManager;       
        private readonly DataGrid _dataGrid;
        private readonly SettingsLoader _settingsLoader;
       
        private Settings.Settings _settings;

        public SettingsWindow(Settings.Settings settings, JsonSettingsManager jsonSettingsManager, DataGrid dataGrid, SettingsLoader settingsLoader)
        {
            InitializeComponent();

            _settings = settings;
            _jsonSettingsManager = jsonSettingsManager;
            _dataGrid = dataGrid;
            _settingsLoader = settingsLoader;

            SetUI();
        }

        private void SetUI()
        {
            _invalidRowsCheckBox.IsChecked = _settings.RemoveInvalidRows;
            _saveColumnsWidthsCheckBox.IsChecked = _settings.SaveColumnWidths;

            PopulateFonts();
            _fontsComboBox.SelectedItem = GetFont();

            if (_settings.RowHightlightColor is not null)
            {
                _rowHighlightColorTextbox.Background = _settings.RowHightlightColor;
            }

        }

        private void PopulateFonts()
        {
            using var installedFonts = new InstalledFontCollection();

            foreach(var fontFamily in installedFonts.Families)
            {
                _fontsComboBox.Items.Add(fontFamily.Name);
            }            
        }

        private string GetFont()
        {
            if (!string.IsNullOrEmpty(_settings.Font))
            {
                return _settings.Font;
            }

            return "Arial";
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            var newSettings = new Settings.Settings
            {
                RemoveInvalidRows = (bool)_invalidRowsCheckBox.IsChecked!,
                SaveColumnWidths = (bool)_saveColumnsWidthsCheckBox.IsChecked!,
                ColumnsWidths = _dataGrid.Columns.Select(x => x.ActualWidth).ToList(),
                Font = _fontsComboBox.SelectedItem.ToString()!,
                RowHightlightColor = _rowHighlightColorTextbox.Background
            };

            _jsonSettingsManager.SaveSettings(newSettings);
            _settingsLoader.LoadSettings();                    

            Close();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void OnTextBoxClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _rowHighlightColorTextbox.Background = new SolidColorBrush(ConvertColorToMediaColor(colorDialog.Color));
            }
        }

        private System.Windows.Media.Color ConvertColorToMediaColor(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
