using AppliedJobsManager.Commands;
using AppliedJobsManager.DataManagement;
using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Models;
using AppliedJobsManager.Settings;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppliedJobsManager.ViewModels
{
    public class AppliedJobsViewModel : ViewModelBase
    {
        private ObservableCollection<Row> _rows;
        private Style _rowHighlightColor;      
        private readonly JsonJobsManager _jsonJobsManager;
        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly InvalidRowsRemover _invalidRowsRemover;
        private readonly InvalidRowsNotifier _invalidRowsNotifier;
        private readonly SettingsLoader _settingsLoader;
        private readonly Settings.Settings _settings;
        

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
        
        public Style RowHighlightColor
        {
            get => _rowHighlightColor;
            set
            {
                _rowHighlightColor = value;
                OnPropertyChanged(nameof(RowHighlightColor));
            }
        }

        public ObservableCollection<DataGridColumn> JobsColumns;

        public System.Windows.Media.FontFamily Font => _settingsLoader.GetFontFamily();

        public AppliedJobsViewModel(ObservableCollection<DataGridColumn> jobsColumns) 
        {
            _jsonJobsManager = new JsonJobsManager();
            _rows = _jsonJobsManager.LoadJobs();

            _invalidRowsRemover = new InvalidRowsRemover(_rows);
            _invalidRowsNotifier = new InvalidRowsNotifier();
            _jsonSettingsManager = new JsonSettingsManager();
            _settingsLoader = new SettingsLoader(_jsonSettingsManager);
            _settings = _jsonSettingsManager.GetSettings();           
            _rowHighlightColor = _settingsLoader.GetRowHightlightColor();

            JobsColumns = jobsColumns;

            LoadColumnWithsIfPossible(jobsColumns);
            ConfigureCommands();
        }

        private void LoadColumnWithsIfPossible(ObservableCollection<DataGridColumn> jobsColumns)
        {
            if (_settings.SaveColumnWidths)
            {
                for (var i = 0; i < _settings.JobsColumns.Count; i++)
                {
                    jobsColumns[i].Width = _settings.JobsColumns[i];
                }
            }
        }

        private void ConfigureCommands()
        {
            OnClosing = new ClosingAppliedJobsCommand(_jsonSettingsManager, _jsonJobsManager, _invalidRowsRemover, _invalidRowsNotifier, _rows);
            OnSettingsClicked = new SettingsClickedCommand(_jsonSettingsManager, _settingsLoader, _settings, this);
            OnAddRow = new AddRowAppliedJobsCommand(this);
            OnRemoveRow = new RemoveRowAppliedJobsCommand(this);
        }

        public ICommand OnAddRow { get; set; }
        public ICommand OnRemoveRow { get; set; }
        public ICommand OnClosing { get; set; }
        public ICommand OnSettingsClicked { get; set; }

    }
}
