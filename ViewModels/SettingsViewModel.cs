using System.Drawing.Text;
using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Settings;
using System.Windows.Input;
using AppliedJobsManager.Commands.SettingsCommands;
using Brush = System.Windows.Media.Brush;

namespace AppliedJobsManager.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private List<int> _suggestedFontSizes = new List<int> { 2, 5, 8, 10, 15, 20 };

        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly SettingsLoader _settingsLoader;
        private readonly AppliedJobsViewModel _appliedJobsViewModel;
        private readonly Settings.Settings _settings;
        
        private Brush _rowHighlightColor;
        private Brush _rowFontColor;
        private bool _saveColumnWithsCheckboxEnabled;
        private string _selectedFont;
        private int _selectedFontSize;
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
      
        public List<string> Fonts => GetFonts();       
        public Brush RowHighlightColor => _rowHighlightColor;
        public Brush RowFontColor => _rowFontColor;
      
        public string SelectedFont
        {
            get => _selectedFont;
            set => _selectedFont = value;
        }

        public int SelectedFontSize
        {
            get => _selectedFontSize;
            set => _selectedFontSize = value;
        }

        public List<int> SuggestedFontSizes
        {
            get => _suggestedFontSizes;
            set => _suggestedFontSizes = value;
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

        public ICommand OkClicked { get; private set; }
        public ICommand CancelClicked { get; private set; }
        public ICommand RowHighlightTextBoxClicked { get; private set; }
        public ICommand FontColorTextBoxClicked { get; private set; }       

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

            SetSuggestedFontSizes();

            _selectedFont = GetSelectedFont();
            _selectedFontSize = GetSelectedFontSize();
        }

        private void SetSuggestedFontSizes()
        {           
            if (_settings.FontSize is not 0 && !_suggestedFontSizes.Contains(_settings.FontSize))
            {
                _suggestedFontSizes.Add(_settings.FontSize);
                _suggestedFontSizes = _suggestedFontSizes.OrderBy(x => x).ToList();
            }          
        }

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

        private int GetSelectedFontSize()
        {
            if (_settings.FontSize is not 0)
            {
                return _settings.FontSize;
            }

            return 10;
        }
    }
}
