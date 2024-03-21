using AppliedJobsManager.Commands.AppliedJobsCommands;
using AppliedJobsManager.DataManagement;
using AppliedJobsManager.HttpProcessing;
using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Models;
using AppliedJobsManager.Settings;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppliedJobsManager.ViewModels
{
    public class AppliedJobsViewModel : ViewModelBase
    {
        private Settings.Settings _settings;
        private ObservableCollection<Row> _rows;       
        private Style _cellStyle;
        private System.Windows.Media.FontFamily _font;
        private int _fontSize;
        
        private readonly JsonJobsManager _jsonJobsManager;
        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly InvalidRowsRemover _invalidRowsRemover;
        private readonly InvalidRowsNotifier _invalidRowsNotifier;
        private readonly SettingsLoader _settingsLoader;
        private readonly RowsProcessor _rowsProcessor;


        public IList<Row> Rows
        {
            get => _rows;           
            set 
            {
                _rows = (ObservableCollection<Row>) value;
                OnPropertyChanged(nameof(Rows));
            }
        }

        public Row SelectedRow { get; set; }
        
        public Style CellStyle
        {
            get => _cellStyle;
            set
            {
                _cellStyle = value;
                OnPropertyChanged(nameof(CellStyle));
            }
        }
        
        public System.Windows.Media.FontFamily Font
        {
            get => _font;
            set
            {
                _font = value;
                OnPropertyChanged(nameof(Font));
            }
        }

        public int FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }
           
        public AppliedJobsViewModel() 
        {
            _jsonJobsManager = new JsonJobsManager();
            _rows = _jsonJobsManager.LoadJobs();

            _invalidRowsRemover = new InvalidRowsRemover(_rows);
            _invalidRowsNotifier = new InvalidRowsNotifier();
            _jsonSettingsManager = new JsonSettingsManager();
            _settingsLoader = new SettingsLoader(_jsonSettingsManager);
            _rowsProcessor = new RowsProcessor(Rows);

            _rowsProcessor.ProcessAdditionalInformation();
            

            LoadSettings();
            ConfigureCommands();
        }

        private void LoadSettings()
        {
            _settings = _jsonSettingsManager.GetSettings();
            _cellStyle = _settingsLoader.GetCellStyle();
            _font = _settingsLoader.GetFontFamily();
            _fontSize = _settingsLoader.GetFontSize();            
        }
        
        private void ConfigureCommands()
        {
            OnClosing = new ClosingAppliedJobsCommand(_jsonSettingsManager, _jsonJobsManager, _invalidRowsRemover, _invalidRowsNotifier, this);
            OnSettingsClicked = new SettingsClickedAppliedJobsCommand(_jsonSettingsManager, _settingsLoader, _settings, this);
            OnAddRow = new AddRowAppliedJobsCommand(this);
            OnRemoveRow = new RemoveRowAppliedJobsCommand(this);
            OnHelpClicked = new HelpClickedAppliedJobsCommand();
            OnImportExcelClicked = new ImportExcelAppliedJobsCommand(this);
            OnExportExcelClicked = new ExportExcelAppliedJobsCommand(Rows);
        }

        public ICommand OnClosing { get; private set; }
        public ICommand OnSettingsClicked { get; private set; }
        public ICommand OnAddRow { get; private set; }
        public ICommand OnRemoveRow { get; private set; }    
        public ICommand OnHelpClicked { get; private set; }
        public ICommand OnImportExcelClicked { get; private set; }
        public ICommand OnExportExcelClicked { get; private set; }
    }
}
