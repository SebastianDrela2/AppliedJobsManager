using AppliedJobsManager.ViewModels;
using System.Windows.Input;

namespace AppliedJobsManager.Commands.AppliedJobsCommands
{
    internal class SaveClickedAppliedJobsCommand : ICommand
    {
        private readonly AppliedJobsViewModel _appliedJobsViewModel;
        private readonly JobsUpdater _jobsUpdater;
      
        public event EventHandler? CanExecuteChanged;

        public SaveClickedAppliedJobsCommand(JobsUpdater jobsUpdater, AppliedJobsViewModel appliedJobsViewModel)
        {           
           _appliedJobsViewModel = appliedJobsViewModel;
           _jobsUpdater = jobsUpdater;
        }

        public bool CanExecute(object? parameter) => true;
        
        public void Execute(object? parameter)
        {
            _jobsUpdater.UpdateJobs();
            _appliedJobsViewModel.RowsAreOutdated = false;
        }      
    }
}
