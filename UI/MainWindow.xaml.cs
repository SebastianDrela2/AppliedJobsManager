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
        private readonly InvalidRowsRemover _invalidRowsRemover;

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
            _invalidRowsRemover = new InvalidRowsRemover(_dataItems);

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
            _invalidRowsRemover.RemoveInvalidRows();
            _jsonJobsManager.SaveJobs(_dataItems);
        }
    }

    public class DataItem
    {
        public string Link { get; set; }
        public string Job { get; set; }
        public string Pay { get; set; }
        public string Date { get; set; }
    }
}