using AppliedJobsManager.ViewModels;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace AppliedJobsManager.Commands.AppliedJobsCommands
{
    public class RemoveRowAppliedJobsCommand : ICommand
    {
        private const string MessageBoxMessage = "Are you sure you want to delete this row? \n This action cannot be reverted.";

        public event EventHandler? CanExecuteChanged;
        private readonly AppliedJobsViewModel _appliedJobsViewModel;

        public RemoveRowAppliedJobsCommand(AppliedJobsViewModel appliedJobsViewModel_)
        {
            _appliedJobsViewModel = appliedJobsViewModel_;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            var messageBoxResult = MessageBox.Show(MessageBoxMessage, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult is MessageBoxResult.Yes)
            {
                _appliedJobsViewModel.Rows.Remove(_appliedJobsViewModel.SelectedRow);
                _appliedJobsViewModel.RowsAreOutdated = true;
            }
        }
    }
}
