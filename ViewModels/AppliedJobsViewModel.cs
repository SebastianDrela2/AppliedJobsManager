using AppliedJobsManager.Commands;
using AppliedJobsManager.Commands.AppliedJobsCommands;
using AppliedJobsManager.HttpProcessing;
using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Models;
using AppliedJobsManager.Settings;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FontFamily = System.Windows.Media.FontFamily;

namespace AppliedJobsManager.ViewModels
{
    public class AppliedJobsViewModel : ViewModelBase
    {
        private Settings.Settings _settings;
        private ObservableCollection<Row> _rows;       
        private Style _cellStyle;
        private FontFamily _font;
        private int _fontSize;

        private readonly JobsUpdater _jobsUpdater;
        private readonly JsonJobsManager _jsonJobsManager;
        private readonly JsonSettingsManager _jsonSettingsManager;       
        private readonly SettingsLoader _settingsLoader;      

        public bool RowsAreOutdated;

        public IList<Row> Rows
        {
            get => _rows;           
            set 
            {
                _rows = new ObservableCollection<Row>(value);
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
        
        public FontFamily Font
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

            var rowsProcessor = new RowsProcessor(Rows);
            rowsProcessor.ProcessAdditionalInformation();
        
            _jsonSettingsManager = new JsonSettingsManager();
            _settingsLoader = new SettingsLoader(_jsonSettingsManager);
            _jobsUpdater = new JobsUpdater(_jsonSettingsManager, _jsonJobsManager, this);
                      
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
            var importExcelCommand = new ImportExcelAppliedJobsCommand(this);

            OnClosing = new ClosingAppliedJobsCommand(_jsonSettingsManager, _jobsUpdater, this);
            OnSettingsClicked = new SettingsClickedAppliedJobsCommand(_jsonSettingsManager, _settingsLoader, _settings, this);
            OnAddRowClicked = new AddRowAppliedJobsCommand(this);
            OnRemoveRowClicked = new RemoveRowAppliedJobsCommand(this);
            OnHelpClicked = new HelpClickedAppliedJobsCommand();          
            OnImportExcelClicked = new DelegateCommand(importExcelCommand.ExecuteAsync);
            OnExportExcelClicked = new ExportExcelAppliedJobsCommand(Rows);
            OnCellRightClicked = new CellRightClickedAppliedJobsCommand();
            OnSaveClicked = new SaveClickedAppliedJobsCommand(_jobsUpdater, this);
        }

        public ICommand OnClosing { get; private set; }
        public ICommand OnSettingsClicked { get; private set; }
        public ICommand OnAddRowClicked { get; private set; }
        public ICommand OnRemoveRowClicked { get; private set; }    
        public ICommand OnHelpClicked { get; private set; }
        public ICommand OnImportExcelClicked { get; private set; }
        public ICommand OnExportExcelClicked { get; private set; }
        public ICommand OnCellRightClicked { get; private set; }
        public ICommand OnSaveClicked { get; private set; }
    }
}
