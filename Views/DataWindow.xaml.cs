using AppliedJobsManager.ViewModels;
using System.Windows;

namespace AppliedJobsManager.Views
{
    /// <summary>
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        private readonly DataViewModel _viewModel;
        public DataWindow(AppliedJobsViewModel appliedJobsViewModel)
        {
            InitializeComponent();

            _viewModel = new DataViewModel(appliedJobsViewModel);

            DataContext = _viewModel;
            Show();
        }

        private void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.OnOkClicked.Execute(this);
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.OnCancelClicked.Execute(this);
        }
    }
}
