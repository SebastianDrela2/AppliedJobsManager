using AppliedJobsManager.DataManagement;
using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.Models;
using AppliedJobsManager.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppliedJobsManager.Commands
{
    public class ClosingAppliedJobsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly JsonJobsManager _jsonJobsManager;
        private readonly InvalidRowsRemover _invalidRowsRemover;
        private readonly InvalidRowsNotifier _invalidRowsNotifier;
        private readonly Settings.Settings _settings;
        private readonly ObservableCollection<Row> _rows;

        public ClosingAppliedJobsCommand
            (JsonSettingsManager jsonSettingsManager, JsonJobsManager jsonJobsManager, 
            InvalidRowsRemover invalidRowsRemover, InvalidRowsNotifier invalidRowsNotifier, ObservableCollection<Row> rows)
        {
            _jsonSettingsManager = jsonSettingsManager;
            _invalidRowsRemover = invalidRowsRemover;
            _invalidRowsNotifier = invalidRowsNotifier;
            _jsonJobsManager = jsonJobsManager;
            _settings = jsonSettingsManager.GetSettings();
            _rows = rows;
        }
        public bool CanExecute(object? parameter) => true;
        
        public void Execute(object? parameter)
        {           
            var view = (AppliedJobsView) parameter!;           

            if (_settings.RemoveInvalidRows)
            {
                var previousItems = _rows.Cast<object>().ToList();
                var invalidRows = _invalidRowsRemover.ManageInvalidRows();

                _invalidRowsNotifier.Notify(invalidRows, previousItems);
                _jsonJobsManager.SaveJobs(_rows);
            }

            if (_settings.SaveColumnWidths)
            {
                _settings.JobsColumns = view._dataGrid.Columns.Select(x => x.ActualWidth).ToList();
            }

            _settings.Window = new Rectangle((int)view.Left, (int)view.Top, (int)view.Width, (int)view.Height);

            _jsonSettingsManager.SaveSettings(_settings);
        }
    }
}
