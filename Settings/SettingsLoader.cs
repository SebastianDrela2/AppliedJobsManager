using AppliedJobsManager.JsonProcessing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppliedJobsManager.Settings
{
    public class SettingsLoader
    {
        private DataGrid _dataGrid;
        private JsonSettingsManager _jsonSettingsManager;                     

        public SettingsLoader(DataGrid dataGrid, JsonSettingsManager jsonSettingsManager)
        {
            _dataGrid = dataGrid;
            _jsonSettingsManager = jsonSettingsManager;           
        }

        public Settings LoadSettings()
        {
            var settings = _jsonSettingsManager.GetSettings();

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
    }
}
