using AppliedJobsManager.DataManagement;
using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.ViewModels;

namespace AppliedJobsManager
{
    public class JobsUpdater
    {       
        private readonly AppliedJobsViewModel _appliedJobsViewModel;
        private readonly InvalidRowsRemover _invalidRowsRemover;
        private readonly InvalidRowsNotifier _invalidRowsNotifier;
        private readonly JsonSettingsManager _jsonSettingsManager;
        private readonly JsonJobsManager _jsonJobsManager;

        public JobsUpdater(JsonSettingsManager jsonSettingsManager, JsonJobsManager jsonJobsManager, 
            AppliedJobsViewModel appliedJobsViewModel)
        {                      
            _jsonSettingsManager = jsonSettingsManager;
            _jsonJobsManager = jsonJobsManager;
            _appliedJobsViewModel = appliedJobsViewModel;

            _invalidRowsRemover = new InvalidRowsRemover(_appliedJobsViewModel);
            _invalidRowsNotifier = new InvalidRowsNotifier();
        }

        public void UpdateJobs()
        {
            var settings = _jsonSettingsManager.GetSettings();

            if (settings.RemoveInvalidRows)
            {
                var previousItems = _appliedJobsViewModel.Rows.ToList();
                var invalidRows = _invalidRowsRemover.ManageInvalidRows();
                _invalidRowsNotifier.Notify(invalidRows, previousItems);
            }

            _jsonJobsManager.SaveJobs(_appliedJobsViewModel.Rows);
        }
    }
}
