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
            
            DataItems = new ObservableCollection<DataItem>
            {
                new() { Link = "Value1", Job = "Value2", Pay = "Value3", Date = "Value4" },
            };

            DataContext = this;

        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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