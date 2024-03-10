using AppliedJobsManager.Views;
using System.Windows.Input;
using System.Windows.Media;

namespace AppliedJobsManager.Commands
{
    public class SettingsRowFontTextboxClickedCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter)
        {
            var view = (SettingsWindow) parameter!;

            var colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                view._rowFontColorTextbox.Background = new SolidColorBrush(ConvertColorToMediaColor(colorDialog.Color));
            }
        }

        private System.Windows.Media.Color ConvertColorToMediaColor(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
