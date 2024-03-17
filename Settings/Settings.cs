using Brush = System.Windows.Media.Brush;

namespace AppliedJobsManager.Settings
{
    public class Settings
    {
        public bool RemoveInvalidRows { get; set; }
        public bool SaveColumnWidths { get; set; }
        public string Font;
        public int FontSize;
        public Brush RowHightlightColor;
        public Brush RowFontColor;
        public List<double> JobsColumns { get; set; }
        public Rectangle Window;
    }
}
