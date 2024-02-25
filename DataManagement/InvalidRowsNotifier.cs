using AppliedJobsManager.UI;
using System.Text;
using System.Windows;

namespace AppliedJobsManager.DataManagement
{
    internal class InvalidRowsNotifier
    {
        public void Notify(List<Row> rows)
        {          
            var stringBuilder = new StringBuilder();

            if (rows.Count == 0)
            {
                return;
            }

            foreach(var row in rows)
            {
                var notifyType = GetNotifyType(row);
                stringBuilder.Append($"Removed {row} because of {notifyType} column \n");
            }
            
            MessageBox.Show(stringBuilder.ToString());
        }

        private string GetNotifyType(Row row)
        {
            if (row.Job is null)
            {
                return "Job";
            }

            if (row.Link is null)
            {
                return "Link";
            }

            if (row.Date is null || !DateTime.TryParse(row.Date, out _))
            {
                return "Date";
            }

            if (row.Pay is null || !int.TryParse(row.Pay, out _))
            {
                return "Pay";
            }

            return null;
        }
    }
}
