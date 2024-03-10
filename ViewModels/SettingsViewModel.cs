using System.Drawing.Text;
using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Settings;
using System.Windows.Input;
using AppliedJobsManager.Commands;

namespace AppliedJobsManager.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {

        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly SettingsLoader _settingsLoader;
        private readonly AppliedJobsViewModel _appliedJobsViewModel;
        private readonly Settings.Settings _settings;
        
        private System.Windows.Media.Brush _rowHighlightColor;
        private System.Windows.Media.Brush _rowFontColor;
        private bool _saveColumnWithsCheckboxEnabled;
        private string _selectedFont;
        private bool _invalidCheckBoxEnabled;

        public SettingsViewModel
            (JsonSettingsManager jsonSettingsManager, SettingsLoader 
            settingsLoader, Settings.Settings settings, AppliedJobsViewModel appliedJobsViewModel)
        {
            _jsonSettingsManager = jsonSettingsManager;
            _settingsLoader = settingsLoader;
            _settings = settings;
            _appliedJobsViewModel = appliedJobsViewModel;

            ConfigureCommands();
            SetUI();
        }

        private void ConfigureCommands()
        {
            OkClicked = new SettingsOkButtonClickedCommand(_jsonSettingsManager, _settingsLoader, _appliedJobsViewModel);
            CancelClicked = new SettingsCancelClickedCommand();
            RowHighlightTextBoxClicked = new SettingsRowHighlightTextboxClickedCommand();
            FontColorTextBoxClicked = new SettingsRowFontTextboxClickedCommand();
        }

        private void SetUI()
        {
           _invalidCheckBoxEnabled = _settings.RemoveInvalidRows;
           _saveColumnWithsCheckboxEnabled = _settings.SaveColumnWidths;

            if (_settings.RowHightlightColor is not null)
            {
                _rowHighlightColor = _settings.RowHightlightColor;
            }

            if (_settings.RowFontColor is not null)
            {
                _rowFontColor = _settings.RowFontColor;
            }

            _selectedFont = GetSelectedFont();
        }
        
        public List<string> Fonts => GetFonts();
        public System.Windows.Media.Brush RowHighlightColor => _rowHighlightColor;
        public System.Windows.Media.Brush RowFontColor => _rowFontColor;

        public string SelectedFont
        {
            get => _selectedFont;
            set => _selectedFont = value;
        }
              
        public bool InvalidRowsCheckBoxEnabled
        {
            get => _invalidCheckBoxEnabled;
            set => _invalidCheckBoxEnabled = value;
        }

        public bool SaveColumnWidthsCheckBoxEnabled
        {
            get => _saveColumnWithsCheckboxEnabled;
            set => _saveColumnWithsCheckboxEnabled = value;
        }

        public ICommand OkClicked { get; set; }
        public ICommand CancelClicked { get; set; }
        public ICommand RowHighlightTextBoxClicked { get; set; }
        public ICommand FontColorTextBoxClicked { get; set; }

        private List<string> GetFonts()
        {
            using var installedFonts = new InstalledFontCollection();
            var fonts = new List<string>();

            foreach (var fontFamily in installedFonts.Families)
            {
                fonts.Add(fontFamily.Name);
            }

            return fonts;
        }

        private string GetSelectedFont()
        {
            if (!string.IsNullOrEmpty(_settings.Font))
            {
                return _settings.Font;
            }

            return "Arial";
        }
    }
}
