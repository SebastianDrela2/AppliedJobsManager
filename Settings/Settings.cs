namespace AppliedJobsManager.Settings
{
    public class Settings
    {
        public bool RemoveInvalidRows { get; set; }
        public bool SaveColumnWidths { get; set; }
        public string Font;
        public System.Windows.Media.Brush RowHightlightColor;

        public List<double> ColumnsWidths { get; set; }       
    }
}
