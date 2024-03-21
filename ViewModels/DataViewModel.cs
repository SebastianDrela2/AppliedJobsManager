using AppliedJobsManager.Commands.DataWindowCommands;
using System.Windows.Input;

namespace AppliedJobsManager.ViewModels
{
    public class DataViewModel : ViewModelBase
    {
        public DataViewModel(AppliedJobsViewModel appliedJobsViewModel)
        {
            OnOkClicked = new DataOnOkClickedCommand(appliedJobsViewModel);
            OnCancelClicked = new DataOnCancelClickedCommand();
        }

        public ICommand OnOkClicked { get; init; }
        public ICommand OnCancelClicked { get; init; }
    }
}
