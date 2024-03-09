using AppliedJobsManager.Views;
using System.Windows.Input;

namespace AppliedJobsManager.Commands
{
    public class SettingsCancelClickedCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;
        
        public void Execute(object? parameter)
        {
            var view = (SettingsWindow) parameter!;

            view.Close();
        }
    }
}
