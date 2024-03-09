using AppliedJobsManager.ViewModels;
using System.Windows;

namespace AppliedJobsManager.Views
{
    /// <summary>
    /// Interaction logic for AppliedJobsView.xaml
    /// </summary>
    public partial class AppliedJobsView : Window
    {
        private readonly AppliedJobsViewModel _viewModel;
        public AppliedJobsView()
        {
            InitializeComponent();
            
            _viewModel = (AppliedJobsViewModel) DataContext;
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _viewModel.OnClosing.Execute(this);
        }
    }
}
