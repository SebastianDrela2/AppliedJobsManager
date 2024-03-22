using System.Windows.Input;

namespace AppliedJobsManager.Commands
{
    internal class DelegateCommand : ICommand
    {        
        private readonly Func<Task> _execute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Func<Task> execute)            
        {
            _execute = execute;
        }
        public bool CanExecute(object parameter) => true;      
        public void Execute(object parameter)
        {
            _execute();
        }       
    }
}
