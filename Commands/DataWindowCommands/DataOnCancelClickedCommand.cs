using AppliedJobsManager.Views;
using System.Windows.Input;

namespace AppliedJobsManager.Commands.DataWindowCommands
{
    internal class DataOnCancelClickedCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            var view = (DataWindow)parameter!;

            view.Close();
        }
    }
}
