﻿using AppliedJobsManager.Commands;
using AppliedJobsManager.Commands.AppliedJobsCommands;
using AppliedJobsManager.DataManagement;
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
        
        private readonly JsonJobsManager _jsonJobsManager;
        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly InvalidRowsRemover _invalidRowsRemover;
        private readonly InvalidRowsNotifier _invalidRowsNotifier;
        private readonly SettingsLoader _settingsLoader;      

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

            _invalidRowsRemover = new InvalidRowsRemover(_rows);
            _invalidRowsNotifier = new InvalidRowsNotifier();
            _jsonSettingsManager = new JsonSettingsManager();
            _settingsLoader = new SettingsLoader(_jsonSettingsManager);
                      
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

            OnClosing = new ClosingAppliedJobsCommand(_jsonSettingsManager, _jsonJobsManager, _invalidRowsRemover, _invalidRowsNotifier, this);
            OnSettingsClicked = new SettingsClickedAppliedJobsCommand(_jsonSettingsManager, _settingsLoader, _settings, this);
            OnAddRow = new AddRowAppliedJobsCommand(this);
            OnRemoveRow = new RemoveRowAppliedJobsCommand(this);
            OnHelpClicked = new HelpClickedAppliedJobsCommand();          
            OnImportExcelClicked = new DelegateCommand(importExcelCommand.ExecuteAsync);
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
