using System.Drawing.Text;
using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Settings;
using System.Windows.Input;
using AppliedJobsManager.Commands;

namespace AppliedJobsManager.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {       

        private readonly Settings.Settings _settings;
        
        private System.Windows.Media.Brush _rowHighlightColor;
        private bool _saveColumnWithsCheckboxEnabled;
        private string _selectedFont;
        private bool _invalidCheckBoxEnabled;

        public SettingsViewModel
            (JsonSettingsManager jsonSettingsManager, SettingsLoader 
            settingsLoader, Settings.Settings settings, AppliedJobsViewModel appliedJobsViewModel)
        {                       
            _settings = settings;

            OkClicked = new SettingsOkButtonClickedCommand(jsonSettingsManager, settingsLoader, appliedJobsViewModel);
            CancelClicked = new SettingsCancelClickedCommand();
            OnTextBoxClicked = new SettingsTextboxClickedCommand();

            SetUI();
        }

        private void SetUI()
        {
           _invalidCheckBoxEnabled = _settings.RemoveInvalidRows;
           _saveColumnWithsCheckboxEnabled = _settings.SaveColumnWidths;

            if (_settings.RowHightlightColor is not null)
            {
                _rowHighlightColor = _settings.RowHightlightColor;
            }

            _selectedFont = GetSelectedFont();
        }       
        public List<string> Fonts => GetFonts();
        public System.Windows.Media.Brush RowHighlightColor => _rowHighlightColor;

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

        public ICommand OkClicked { get; }
        public ICommand CancelClicked { get; }
        public ICommand OnTextBoxClicked { get; }

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
