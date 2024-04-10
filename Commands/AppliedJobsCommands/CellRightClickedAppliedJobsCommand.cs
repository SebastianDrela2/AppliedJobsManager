using AppliedJobsManager.Utils;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using TextBox = System.Windows.Controls.TextBox;

namespace AppliedJobsManager.Commands.AppliedJobsCommands
{
    class CellRightClickedAppliedJobsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object parameter)
        {
            var e = (MouseEventArgs) parameter;
            var depObj = (DependencyObject) e.OriginalSource;

            var cell = DependencyObjectUtils.FindParent<DataGridCell>(depObj);
            var column = cell?.Column;

            if (column?.Header is "Link")
            {
                var cellContent = (cell?.Content is TextBlock textBlock) ? textBlock.Text :
                (cell?.Content is TextBox textBox) ? textBox.Text : cell?.Content.ToString();

                ShowContextMenu(cellContent!);
            }
        }

        private void ShowContextMenu(string link)
        {
            ContextMenu contextMenu = new ContextMenu();

            var menuItem = new MenuItem()
            {
                Header = "Open Link"
            };

            menuItem.Click += OpenLinkEvent;

            contextMenu.Items.Add(menuItem);
            contextMenu.IsOpen = true;

            void OpenLinkEvent(object sender, RoutedEventArgs e)
            {
                Process.Start(new ProcessStartInfo(link)
                {
                    UseShellExecute = true
                });
            }
        }      
    }
}
