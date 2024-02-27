﻿using AppliedJobsManager.DataManagement;
using AppliedJobsManager.JsonProcessing;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace AppliedJobsManager.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Row> _dataItems;
        private readonly JsonJobsManager _jsonJobsManager;
        private readonly InvalidRowsRemover _invalidRowsRemover;
        private readonly InvalidRowsNotifier _invalidRowsNotifier;
        private readonly Settings.Settings _settings;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Row> DataItems
        {
            get => _dataItems;
            set
            {
                _dataItems = value;
                OnPropertyChanged(nameof(DataItems));
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            _jsonJobsManager = new JsonJobsManager();
            _dataItems = _jsonJobsManager.LoadJobs();
            _invalidRowsRemover = new InvalidRowsRemover(_dataItems);
            _invalidRowsNotifier = new InvalidRowsNotifier();

            var jsonSettingsManager = new JsonSettingsManager();
            _settings = jsonSettingsManager.LoadSettings();

            LoadColumnWidthsIfPossible();

            if (!string.IsNullOrEmpty(_settings.Font))
            {
                _dataGrid.FontFamily = new System.Windows.Media.FontFamily(_settings.Font);
            }

            DataContext = this;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnAddRowClicked(object sender, RoutedEventArgs e)
        {
            _dataItems.Add(new Row());
        }

        private void OnDeleteRowClicked(object sender, RoutedEventArgs e)
        {
            Row dataItem = null;

            try
            {
                dataItem = (Row)_dataGrid.SelectedItem;
            }
            catch(InvalidCastException)
            {
                // selected row is invalid, handle this later.
            }

            _dataItems.Remove(dataItem);           
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (_settings.RemoveInvalidRows)
            {
                var previousItems = _dataGrid.Items.Cast<object>().ToList();
                var invalidRows = _invalidRowsRemover.ManageInvalidRows();

                _invalidRowsNotifier.Notify(invalidRows, previousItems);
                _jsonJobsManager.SaveJobs(_dataItems);
            }
        }

        private void OnSettingsMenuItemClicked(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(_settings, new JsonSettingsManager(), _dataGrid);
            settingsWindow.Show();
        }
        
        private void LoadColumnWidthsIfPossible()
        {
            if (_settings.SaveColumnWidths)
            {
                var index = 0;

                foreach (var column in _dataGrid.Columns)
                {
                    column.Width = _settings.ColumnsWidths[index];
                    index++;
                }
               
            }
        }        
    }

    public class Row
    {
        public string Link { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }
        public string Pay { get; set; }
        public string Date { get; set; }
    }
}