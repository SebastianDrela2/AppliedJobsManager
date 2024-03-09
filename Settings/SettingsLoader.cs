using AppliedJobsManager.JsonProcessing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppliedJobsManager.Settings
{
    public class SettingsLoader
    {       
        private readonly JsonSettingsManager _jsonSettingsManager;
        private Settings _settings;       

        public SettingsLoader(JsonSettingsManager jsonSettingsManager)
        {
            _jsonSettingsManager = jsonSettingsManager;
            _settings = jsonSettingsManager.GetSettings();               
        }
      
        public System.Windows.Media.FontFamily GetFontFamily()
        {
            if (!string.IsNullOrEmpty(_settings.Font))
            {
                return new System.Windows.Media.FontFamily(_settings.Font);
            }

            return new System.Windows.Media.FontFamily("Arial");
        }

        public Style GetRowHightlightColor()
        {
            _settings = _jsonSettingsManager.GetSettings();

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

                return cellStyle;
            }

            return new Style(typeof(DataGridCell));
        }

        public List<double> GetColumnWidths() => _settings.JobsColumns;       
    }
}
