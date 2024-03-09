using AppliedJobsManager.DataManagement;
using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Models;
using AppliedJobsManager.Settings;
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
        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly InvalidRowsRemover _invalidRowsRemover;
        private readonly InvalidRowsNotifier _invalidRowsNotifier;
        private readonly SettingsLoader _settingsLoader;

        private Settings.Settings _settings;

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
            _jsonSettingsManager = new JsonSettingsManager();
            _settingsLoader = new SettingsLoader(_dataGrid, _jsonSettingsManager, this);

            _settings =  _settingsLoader.LoadSettings();

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

            if (_settings.SaveColumnWidths)
            {
                _settings.ColumnsWidths = _dataGrid.Columns.Select(x => x.ActualWidth).ToList();               
            }

            _settings.Window = new Rectangle((int)Left, (int)Top, (int)Width, (int)Height);

            _jsonSettingsManager.SaveSettings(_settings);
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void OnSettingsMenuItemClicked(object sender, RoutedEventArgs e)
        {
            _settings = _jsonSettingsManager.GetSettings();

            var settingsWindow = new SettingsWindow(_settings, new JsonSettingsManager(), _dataGrid, _settingsLoader);
            settingsWindow.Show();
        }       
    }   
}