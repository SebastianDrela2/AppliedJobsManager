using System.Windows.Input;

namespace AppliedJobsManager.Commands
{
    public class HelpClickedAppliedJobsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;      
        public void Execute(object? parameter) => ShowHelp();
       
        private void ShowHelp()
        {
            MessageBox.Show($"Currently only for the feed we support links from the Justjoin.it \n" +
                @"if site is different values will be displayed as not applicable.", "Help"
                ,MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}
