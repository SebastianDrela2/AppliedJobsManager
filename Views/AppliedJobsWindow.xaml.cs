using AppliedJobsManager.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AppliedJobsManager.JsonProcessing;

namespace AppliedJobsManager.Views
{
    /// <summary>
    /// Interaction logic for AppliedJobsView.xaml
    /// </summary>
    public partial class AppliedJobsView : Window
    {
        private readonly AppliedJobsViewModel _viewModel;
        private readonly Settings.Settings _settings;

        public AppliedJobsView()
        {
            InitializeComponent();

            _viewModel = new AppliedJobsViewModel();
            _settings = new JsonSettingsManager().GetSettings();

            LoadColumnWidthsIfPossible(_dataGrid.Columns);
            LoadWindowSizeIfPossible();

            DataContext = _viewModel;
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _viewModel.OnClosing.Execute(this);
        }

        private void LoadColumnWidthsIfPossible(ObservableCollection<DataGridColumn> jobsColumns)
        {
            if (_settings.SaveColumnWidths && _settings.JobsColumns is not null)
            {
                for (var i = 0; i < _settings.JobsColumns.Count; i++)
                {
                    jobsColumns[i].Width = _settings.JobsColumns[i];
                }
            }
        }

        private void LoadWindowSizeIfPossible()
        {
            if (!_settings.Window.IsEmpty)
            {
                Left = _settings.Window.Left;
                Top = _settings.Window.Top;
                Width = _settings.Window.Width;
                Height = _settings.Window.Height;
            }
        }
    }
}
