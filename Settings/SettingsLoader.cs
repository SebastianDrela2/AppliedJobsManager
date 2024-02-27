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
        private Settings _settings;               

        public SettingsLoader(DataGrid dataGrid, JsonSettingsManager jsonSettingsManager, Settings settings)
        {
            _dataGrid = dataGrid;
            _jsonSettingsManager = jsonSettingsManager;
            _settings = settings;
        }

        public void LoadSettings()
        {
            _settings = _jsonSettingsManager.GetSettings();

            LoadColumnWidthsIfPossible();
            LoadRowHightlightColorIfPossible();

            if (!string.IsNullOrEmpty(_settings.Font))
            {
                _dataGrid.FontFamily = new System.Windows.Media.FontFamily(_settings.Font);
            }
        }

        private void LoadRowHightlightColorIfPossible()
        {
            if (_settings.RowHightlightColor is not null)
            {
                var cellStyle = new Style(typeof(DataGridCell));

                var isSelectedTrigger = new Trigger
                {
                    Property = DataGridCell.IsSelectedProperty,
                    Value = true
                };

                isSelectedTrigger.Setters.Add(new Setter(DataGrid.BackgroundProperty, (SolidColorBrush)_settings.RowHightlightColor));
                cellStyle.Triggers.Add(isSelectedTrigger);

                _dataGrid.CellStyle = cellStyle;
            }
        }

        private void LoadColumnWidthsIfPossible()
        {
            if (_settings.SaveColumnWidths)
            {
                var index = 0;

                foreach (var column in _dataGrid.Columns)
                {
                    column.Width = _settings.ColumnsWidths[index];
                    index++;
                }
            }
        }
    }
}
