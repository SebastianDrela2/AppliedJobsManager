using AppliedJobsManager.ViewModels;
using System.Windows.Input;
using AppliedJobsManager.Views;

namespace AppliedJobsManager.Commands
{
    public class AddRowAppliedJobsCommand : ICommand
    {
        private readonly AppliedJobsViewModel _appliedJobsViewmodel;

        public AddRowAppliedJobsCommand(AppliedJobsViewModel appliedJobsViewModel)
        {
            _appliedJobsViewmodel = appliedJobsViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;        
        public void Execute(object? parameter)
        {
            _ = new DataWindow(_appliedJobsViewmodel);           
        }
    }
}
