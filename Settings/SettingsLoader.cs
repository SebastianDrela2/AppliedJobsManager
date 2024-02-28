using AppliedJobsManager.JsonProcessing;
using AppliedJobsManager.UI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppliedJobsManager.Settings
{
    public class SettingsLoader
    {
        private DataGrid _dataGrid;
        private JsonSettingsManager _jsonSettingsManager;
        private MainWindow _mainWindow;

        public SettingsLoader(DataGrid dataGrid, JsonSettingsManager jsonSettingsManager, MainWindow mainWindow)
        {
            _dataGrid = dataGrid;
            _jsonSettingsManager = jsonSettingsManager;   
            _mainWindow = mainWindow;
        }

        public Settings LoadSettings()
        {
            var settings = _jsonSettingsManager.GetSettings();

            LoadWindowSize(settings);
            LoadColumnWidthsIfPossible(settings);
            LoadRowHightlightColorIfPossible(settings);

            if (!string.IsNullOrEmpty(settings.Font))
            {
                _dataGrid.FontFamily = new System.Windows.Media.FontFamily(settings.Font);
            }

            return settings;
        }

        private void LoadRowHightlightColorIfPossible(Settings settings)
        {
            if (settings.RowHightlightColor is not null)
            {
                var cellStyle = new Style(typeof(DataGridCell));

                var isSelectedTrigger = new Trigger
                {
                    Property = DataGridCell.IsSelectedProperty,
                    Value = true
                };

                isSelectedTrigger.Setters.Add(new Setter(DataGrid.BackgroundProperty, (SolidColorBrush)settings.RowHightlightColor));
                cellStyle.Triggers.Add(isSelectedTrigger);

                _dataGrid.CellStyle = cellStyle;
            }
        }

        private void LoadColumnWidthsIfPossible(Settings settings)
        {
            if (settings.SaveColumnWidths)
            {
                var index = 0;

                foreach (var column in _dataGrid.Columns)
                {
                    column.Width = settings.ColumnsWidths[index];
                    index++;
                }
            }
        }

        private void LoadWindowSize(Settings settings)
        {
            if (settings.Window.Size.Width is not 0)
            {
                _mainWindow.Left = settings.Window.Left;
                _mainWindow.Top = settings.Window.Top;
                _mainWindow.Width = settings.Window.Width;
                _mainWindow.Height = settings.Window.Height;
            }
        }
    }
}
