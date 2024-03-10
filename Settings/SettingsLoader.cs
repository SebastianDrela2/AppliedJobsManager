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

        public void UpdateSettings()
        {
            _settings = _jsonSettingsManager.GetSettings();
        }
      
        public System.Windows.Media.FontFamily GetFontFamily()
        {
            if (!string.IsNullOrEmpty(_settings.Font))
            {
                return new System.Windows.Media.FontFamily(_settings.Font);
            }

            return new System.Windows.Media.FontFamily("Arial");
        }

        public Style GetCellStyle()
        {
            var cellStyle = new Style(typeof(DataGridCell));


            if (_settings.RowFontColor is not null)
            {
                cellStyle.Setters.Add(new Setter(DataGridCell.ForegroundProperty, _settings.RowFontColor));
            }

            if (_settings.RowHightlightColor is not null)
            {                
                var isSelectedTrigger = new Trigger
                {
                    Property = DataGridCell.IsSelectedProperty,
                    Value = true
                };

                isSelectedTrigger.Setters.Add(new Setter(DataGrid.BackgroundProperty, (SolidColorBrush)_settings.RowHightlightColor));
                cellStyle.Triggers.Add(isSelectedTrigger);             
            }

            return cellStyle;
        }

        public System.Windows.Media.Brush GetRowFontColor()
        {
            if (_settings.RowFontColor is not null)
            {
                return _settings.RowFontColor;
            }

            // black brush default
            return new SolidColorBrush(System.Windows.Media.Color.FromArgb(0,0,0,0));
        }

        public List<double> GetColumnWidths() => _settings.JobsColumns;       
    }
}
