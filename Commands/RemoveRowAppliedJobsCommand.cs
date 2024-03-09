using AppliedJobsManager.ViewModels;
using System.Windows.Input;

namespace AppliedJobsManager.Commands
{
    public class RemoveRowAppliedJobsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly AppliedJobsViewModel _appliedJobsViewModel;

        public RemoveRowAppliedJobsCommand(AppliedJobsViewModel appliedJobsViewModel_)
        {
            _appliedJobsViewModel = appliedJobsViewModel_;
        }

        public bool CanExecute(object? parameter) => true;
       
        public void Execute(object? parameter)
        {
            _appliedJobsViewModel.Rows.Remove(_appliedJobsViewModel.SelectedRow);
        }
    }
}
