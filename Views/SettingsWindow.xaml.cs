using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Settings;
using AppliedJobsManager.ViewModels;
using System.Windows;

namespace AppliedJobsManager.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {        
        private readonly SettingsViewModel _viewModel;       

        public SettingsWindow
            (JsonSettingsManager jsonSettingsManager, 
            SettingsLoader settingsLoader, Settings.Settings settings, AppliedJobsViewModel appliedJobsViewModel)
        {            
            InitializeComponent();

            _viewModel = new SettingsViewModel(jsonSettingsManager, settingsLoader, settings, appliedJobsViewModel);
           
            DataContext = _viewModel;
        }
        
        public void OnOkButtonClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.OkClicked.Execute(this);
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.CancelClicked.Execute(this);
        }

        private void OnRowHighlightTextBoxClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _viewModel.RowHighlightTextBoxClicked.Execute(this);
        }

        private void OnRowFontTextBoxClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _viewModel.FontColorTextBoxClicked.Execute(this);
        }

        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key is System.Windows.Input.Key.Enter or System.Windows.Input.Key.Space)
            {
                _viewModel.OkClicked.Execute(this);
            }
        }
    }
}
