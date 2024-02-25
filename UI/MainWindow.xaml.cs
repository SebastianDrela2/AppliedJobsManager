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
        private ObservableCollection<Row> _dataItems;
        private readonly JsonJobsManager _jsonJobsManager;
        private readonly InvalidRowsRemover _invalidRowsRemover;

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
            _invalidRowsRemover.RemoveInvalidRows();
            _jsonJobsManager.SaveJobs(_dataItems);
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