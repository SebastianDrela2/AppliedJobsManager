using AppliedJobsManager.DataManagement;
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
        private ObservableCollection<DataItem> _dataItems;
        private readonly JsonJobsManager _jsonJobsManager;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<DataItem> DataItems
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
            DataContext = this;

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnAddRowClicked(object sender, RoutedEventArgs e)
        {
            _dataItems.Add(new DataItem());
        }

        private void OnDeleteRowClicked(object sender, RoutedEventArgs e)
        {
            var dataItem = (DataItem)_dataGrid.SelectedItem ;
            _dataItems.Remove(dataItem);
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            RemoveCorruptedRows();

            _jsonJobsManager.SaveJobs(_dataItems);
        }

        private void RemoveCorruptedRows()
        {
            var corruptedDataItems = _dataItems.Where(x => x.Date is null || x.Job is null || x.Pay is null || x.Link is null).ToList();

            foreach (var corruptedItem in corruptedDataItems)
            {
                _dataItems.Remove(corruptedItem);
            }
        }
    }

    public struct DataItem
    {
        public string Link { get; set; }
        public string Job { get; set; }
        public string Pay { get; set; }
        public string Date { get; set; }
    }
}